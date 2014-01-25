using Microsoft.Phone.Tasks;
using System;
using System.Threading.Tasks;
using WindowsCommonLibrary.PCL.Interfaces;
using WindowsCommonLibrary.WindowsPhone.Helpers;

namespace WindowsCommonLibrary.WindowsPhone.Common
{
    public class PhotoService : IPhotoService
    {
        private TaskCompletionSource<byte[]> _tcs;

        public Task<byte[]> TakePhotoFromCamera()
        {
            _tcs = new TaskCompletionSource<byte[]>();

            CameraCaptureTask cameraCaptureTask = new CameraCaptureTask();
            cameraCaptureTask.Completed += new EventHandler<PhotoResult>(cameraCaptureTask_Completed);
            cameraCaptureTask.Show();

            return _tcs.Task;
        }

        private void cameraCaptureTask_Completed(object sender, PhotoResult e)
        {
            if (e.Error != null)
            {
                _tcs.SetException(e.Error);
                return;
            }
            
            var bytes = StreamHelper.ReadToEnd(e.ChosenPhoto);
            _tcs.SetResult(bytes);
        }

        public Task<byte[]> TakePhotoFromDisc()
        {
            _tcs = new TaskCompletionSource<byte[]>();

            PhotoChooserTask photoChooserTask = new PhotoChooserTask();
            photoChooserTask.Completed += cameraCaptureTask_Completed;
            photoChooserTask.Show();
            return _tcs.Task;

        }
    }
}
