using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsCommonLibrary.PCL.Interfaces;

namespace WindowsCommonLibrary.WindowsStore.Common
{
    public class VideoService : IVideoService
    {
        public PCL.Utils.RecordingState RecordingState
        {
            get { throw new NotImplementedException(); }
        }

        public void StartRecording()
        {
            throw new NotImplementedException();
        }

        public void PauseRecording()
        {
            throw new NotImplementedException();
        }

        public void StopRecording()
        {
            throw new NotImplementedException();
        }

        public System.IO.Stream GetVideo()
        {
            throw new NotImplementedException();
        }

        public Task<System.IO.Stream> GetVideoAsync()
        {
            throw new NotImplementedException();
        }

        public Task TakeVideoFromDisc()
        {
            throw new NotImplementedException();
        }
    }
}
