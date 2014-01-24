using System.IO;
using System.Runtime.Serialization.Json;

namespace WindowsCommonLibrary.PCL.Utils
{
    /// <summary>
    /// Generic class that serialize and deserialize object using json in one line. 
    /// </summary>
    /// <typeparam name="T">Type of object which will be serialize or deserialize</typeparam>
    public class JsonSerializer<T>
        where T : class
    {

        /// <summary>
        /// Create object from string which contains json.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public T Deserialize(string text)
        {

            Stream objectStream = new MemoryStream();
            using (var sw = new StreamWriter(objectStream))
            {
                sw.Write(text);
                sw.Flush();
                objectStream.Position = 0;

                var serializer = new DataContractJsonSerializer(typeof(T));
                var sObject = serializer.ReadObject(objectStream) as T;
                return sObject;
            }
        }

        /// <summary>
        /// Create object from stream which contains json.
        /// </summary>
        /// <param name="jsonStream"></param>
        /// <returns></returns>
        public T Deserialize(Stream jsonStream)
        {
            var serializer = new DataContractJsonSerializer(typeof(T));
            var sObject = serializer.ReadObject(jsonStream) as T;
            return sObject;
        }


        /// <summary>
        /// Create json code from object. 
        /// </summary>
        /// <param name="objectToSerialize"></param>
        /// <returns></returns>
        public string Serialize(T objectToSerialize)
        {
            using (var stream = new MemoryStream())
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                serializer.WriteObject(stream, objectToSerialize);
                stream.Position = 0;
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }

        }
    }
}
