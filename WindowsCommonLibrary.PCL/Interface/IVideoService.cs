using System.IO;
using System.Threading.Tasks;
using WindowsCommonLibrary.PCL.Utils;

namespace WindowsCommonLibrary.PCL.Interfaces
{
    public interface IVideoService
    {
        RecordingState RecordingState { get; }

        void StartRecording();

        void PauseRecording();

        void StopRecording();

        Stream GetVideo();

        Task<Stream> GetVideoAsync();

        Task TakeVideoFromDisc();
    }
}
