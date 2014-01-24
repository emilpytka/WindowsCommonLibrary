using System.IO;
using System.Threading.Tasks;
using WindowsCommonLibrary.PCL.Utils;

namespace WindowsCommonLibrary.PCL.Interfaces
{
    public interface IAudioService
    {
        RecordingState RecordingState { get; }

        Task StartRecording();

        Task PauseRecording();

        Task StopRecording();

        Stream GetAudio();

        event WindowsCommonLibrary.PCL.Utils.Delegate.RecordingFailed RecordingFailed;

        Task<Stream> GetAudioAsync();

        Task<Stream> GetAudioFromDisc();
    }

    
}
