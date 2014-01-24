using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage.Streams;
using WindowsCommonLibrary.PCL.Interfaces;
using WindowsCommonLibrary.PCL.Utils;

namespace WindowsCommonLibrary.WindowsStore.Common
{
    //TODO: Tests needed
    public class AudioService : IAudioService
    {
        private MediaCapture _mediaCaptureMgr;
        private bool _isDeviceStarted;
        private IRandomAccessStream _stream;

        public AudioService()
        {
            RecordingState = RecordingState.Stop;
        }

        private RecordingState _recordingState;
        public RecordingState RecordingState
        {
            get { return _recordingState; }
            private set { _recordingState = value; }
        }

        public event PCL.Utils.Delegate.RecordingFailed RecordingFailed;

        /// <summary>
        /// Run Device and Start recording
        /// </summary>
        /// <returns></returns>
        public async Task StartRecording()
        {
            if (RecordingState == RecordingState.Start)
                return;

            RecordingState = RecordingState.Start;

            if (!_isDeviceStarted)
                await StartDevice();

            _stream = new InMemoryRandomAccessStream();
            var recordProfile = MediaEncodingProfile.CreateMp3(Windows.Media.MediaProperties.AudioEncodingQuality.Auto);
            await _mediaCaptureMgr.StartRecordToStreamAsync(recordProfile, _stream);
        }

        /// <summary>
        /// Pausing is unavailable in MediaCapture
        /// </summary>
        /// <returns></returns>
        public Task PauseRecording()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Stop recording and dispose MediaCapture
        /// </summary>
        /// <returns></returns>
        public async Task StopRecording()
        {
            RecordingState = RecordingState.Stop;

            await _mediaCaptureMgr.StopRecordAsync();
            _mediaCaptureMgr.Dispose();
        }

        /// <summary>
        /// Get Audio as Stream
        /// </summary>
        /// <returns></returns>
        public System.IO.Stream GetAudio()
        {
            var result = _stream.AsStream();
            result.Position = 0;
            return result;
        }

        /// <summary>
        /// Get Audio as Stream - there is no async metod to convert 
        /// IRandomAccessStream to Stream.
        /// </summary>
        /// <returns></returns>
        public async Task<Stream> GetAudioAsync()
        {
            var result = _stream.AsStream();
            result.Position = 0;
            return result;
        }

        public Task<System.IO.Stream> GetAudioFromDisc()
        {
            throw new NotImplementedException();
        }

        #region Private Methods
        private async Task StartDevice()
        {
            _isDeviceStarted = true;
            _mediaCaptureMgr = new Windows.Media.Capture.MediaCapture();
            var settings = new Windows.Media.Capture.MediaCaptureInitializationSettings();
            settings.StreamingCaptureMode = Windows.Media.Capture.StreamingCaptureMode.Audio;
            settings.MediaCategory = Windows.Media.Capture.MediaCategory.Other;
            settings.AudioProcessing = Windows.Media.AudioProcessing.Default;

            await _mediaCaptureMgr.InitializeAsync(settings);

            _mediaCaptureMgr.RecordLimitationExceeded += new Windows.Media.Capture.RecordLimitationExceededEventHandler(RecordLimitationExceeded);
            _mediaCaptureMgr.Failed += new Windows.Media.Capture.MediaCaptureFailedEventHandler(Failed);
        }

        private void Failed(MediaCapture sender, MediaCaptureFailedEventArgs errorEventArgs)
        {
            RecordingState = RecordingState.Error;
            if (RecordingFailed != null)
            {
                RecordingFailed.Invoke(
                    new AudioServiceFailedEventArgs { 
                        ErrorCode = (int)ErrorCodes.RecordingAudioFiled, 
                        Message = errorEventArgs.Message
                });
            }
        }

        private void RecordLimitationExceeded(MediaCapture sender)
        {
            RecordingState = RecordingState.Error;
            if (RecordingFailed != null)
            {
                RecordingFailed.Invoke(
                    new AudioServiceFailedEventArgs
                    {
                        ErrorCode = (int)ErrorCodes.RecordingAudioLimitationExceeded
                    });
            }
        }
        #endregion
    }
}
