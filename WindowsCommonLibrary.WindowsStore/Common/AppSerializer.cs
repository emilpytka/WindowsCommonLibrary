using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using WindowsCommonLibrary.PCL.Interface;
using WindowsCommonLibrary.PCL.Utils;

namespace WindowsCommonLibrary.WindowsStore.Common
{
    /// <summary>
    /// Class that can serialize and deserialize objects using json. 
    /// Only what you need to do is to send application StorageFolder in constructor. 
    /// </summary>
    /// <typeparam name="T">Type of object to serialize</typeparam>
    public class AppSerializer<T> : IAppSerializer<T>
        where T : class
    {
        private readonly StorageFolder _folder;

        public AppSerializer(StorageFolder folder)
        {
            _folder = folder;
        }

        /// <summary>
        /// Save object to file.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public async Task SaveObjectAsync(T obj, string path)
        {
            var file = await _folder.CreateFileAsync(path, CreationCollisionOption.ReplaceExisting);
            using (var stream = await file.OpenStreamForWriteAsync())
            {
                var serializer = new JsonSerializer<T>();
                var json = serializer.Serialize(obj);

                using (var streamWriter = new StreamWriter(stream))
                {
                    streamWriter.Write(json);
                }
            }
        }

        /// <summary>
        /// Return object saved in file which name is send in parameter.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public async Task<T> LoadObjectAsync(string path)
        {
            var file = await _folder.GetFileAsync(path);

            T result = null;

            if (file != null)
            {
                using (var reader = await file.OpenReadAsync())
                {
                    using (var streamReader = new StreamReader(reader.AsStream()))
                    {
                        var text = streamReader.ReadToEnd();

                        var serializer = new JsonSerializer<T>();
                        result = serializer.Deserialize(text);
                    }
                }
            }
            return result;
        }
    }
}
