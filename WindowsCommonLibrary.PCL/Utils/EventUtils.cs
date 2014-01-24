
namespace WindowsCommonLibrary.PCL.Utils
{
    public class Delegate
    {
        public delegate void RecordingFailed(AudioServiceFailedEventArgs arg);
    }

    public class AudioServiceFailedEventArgs
    {
        public int ErrorCode { get; set; }
        public string Message { get; set; }
    }

    public enum ErrorCodes
    {
        RecordingAudioFiled,
        RecordingAudioLimitationExceeded
    }

}
