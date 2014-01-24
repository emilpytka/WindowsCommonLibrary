using System.Threading.Tasks;

namespace WindowsCommonLibrary.PCL.Interface
{
    /// <summary>
    /// Interface for saving and loading object from/to mobile storage. 
    /// It's different in both Windows 8 and Windows Phone 8 SDK, so it has to be 
    /// represent as interface.
    /// </summary>
    /// <typeparam name="T">Type of object we want to serialize</typeparam>
    public interface IAppSerializer<T>
        where T : class
    {
        Task SaveObjectAsync(T obj, string path);

        Task<T> LoadObjectAsync(string path);
    }
}
