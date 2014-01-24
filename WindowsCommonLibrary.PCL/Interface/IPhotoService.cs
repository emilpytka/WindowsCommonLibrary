using System.Threading.Tasks;

namespace WindowsCommonLibrary.PCL.Interfaces
{
    public interface IPhotoService
    {
        Task<byte[]> TakePhotoFromCamera();

        Task<byte[]> TakePhotoFromDisc();
    }
}
