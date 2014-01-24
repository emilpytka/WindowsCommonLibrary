using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using WindowsCommonLibrary.PCL.Interfaces;

namespace WindowsCommonLibrary.WindowsStore.Common
{
    public class PhotoService : IPhotoService
    {
        public CameraPhotoConfiguration CameraPhotoConfiguration { get; set; }
        public DiscPhotoConfiguration DiscPhotoConfiguration { get; set; }

        public PhotoService()
        {
            CameraPhotoConfiguration = new CameraPhotoConfiguration();
            DiscPhotoConfiguration = new DiscPhotoConfiguration();
        }

        #region IPhotoService
        
        public async Task<byte[]> TakePhotoFromCamera()
        {
            var file = await GetStorageFileFromCamera();
            return await GetBytesFromStorageFile(file);
        }

        public async Task<byte[]> TakePhotoFromDisc()
        {
            StorageFile file = await GetStorageFileFromDisc();
            return await GetBytesFromStorageFile(file);
        }

        #endregion

        private async Task<byte[]> GetBytesFromStorageFile(StorageFile file)
        {
            var stream = await file.OpenReadAsync();

            using (var dataReader = new DataReader(stream))
            {
                var bytes = new byte[stream.Size];
                await dataReader.LoadAsync((uint)stream.Size);
                dataReader.ReadBytes(bytes);

                return bytes;
            }
        }

        private async Task<StorageFile> GetStorageFileFromCamera()
        {
            CameraCaptureUI dialog = new CameraCaptureUI();
            dialog.PhotoSettings.CroppedAspectRatio = this.CameraPhotoConfiguration.AspectRatio;
            return await dialog.CaptureFileAsync(CameraCaptureUIMode.Photo);
        }

        private async Task<StorageFile> GetStorageFileFromDisc()
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            foreach (var fileType in DiscPhotoConfiguration.FileTypes)
            {
                openPicker.FileTypeFilter.Add(fileType);
            }

            return await openPicker.PickSingleFileAsync();
        }

    }

    /// <summary>
    /// Configuration properties for camera capture
    /// </summary>
    public class CameraPhotoConfiguration
    {
        public Size AspectRatio { get; set; }

        public CameraPhotoConfiguration()
        {
            AspectRatio = new Size(16, 9);
        }
    }


    /// <summary>
    /// Configuration properties for picture file chooser
    /// </summary>
    public class DiscPhotoConfiguration
    {
        public List<string> FileTypes { get; set; }
        
        public DiscPhotoConfiguration()
        {
            FileTypes = new List<string> { 
                ".jpg", ".jpeg", ".png"
            };
        }
    }
}
