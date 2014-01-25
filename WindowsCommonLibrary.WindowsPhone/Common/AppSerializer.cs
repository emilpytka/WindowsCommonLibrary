using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WindowsCommonLibrary.PCL.Interface;

namespace WindowsCommonLibrary.WindowsPhone.Common
{
    public class AppSerializer<T> : IAppSerializer<T>
        where T : class
    {
        public async Task SaveObjectAsync(T obj, string path)
        {
            if (String.IsNullOrEmpty(path) || obj == null)
                return;

            using (var isolated = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (isolated.FileExists(path)) isolated.DeleteFile(path);

                Stream stream = null;
                try
                {
                    stream = isolated.OpenFile(path, FileMode.OpenOrCreate, FileAccess.Write);
                    var serializer = new XmlSerializer(typeof(T));
                    serializer.Serialize(stream, obj);
                }
                finally
                {
                    if (stream != null) stream.Close();
                }
            }
        }

        public async Task<T> LoadObjectAsync(string path)
        {
            T result = null;
            if (!String.IsNullOrEmpty(path))
            {
                using (var isolated = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    if (isolated.FileExists(path))
                    {
                        Stream stream = null;
                        try
                        {
                            stream = isolated.OpenFile(path, FileMode.Open, FileAccess.Read);
                            var serializer = new XmlSerializer(typeof(T));
                            result = serializer.Deserialize(stream) as T;
                        }
                        finally
                        {
                            if (stream != null) stream.Close();
                        }
                    }
                }
            }
            return result;
        }
    }
}
