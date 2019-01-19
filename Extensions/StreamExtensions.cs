namespace Extensions
{
    using System.IO;
    using System.Text;
    using System;
    using Newtonsoft.Json;

    public static class StreamExtensions
    {
        public static void WriteObjectToStream<T>(this Stream stream, T objectToWrite)
        {
            using(var streamWriter = new StreamWriter(stream, new UTF8Encoding(), 1028, true))
            {
                using(var jsonTextWriter = new JsonTextWriter(streamWriter))
                {
                    var jsonResponse = new JsonSerializer();
                    jsonResponse.Serialize(jsonTextWriter, objectToWrite);
                    jsonTextWriter.Flush();
                }
            }
        }

        public static T ReadResponseFromStreamAndDeserialize<T>(this Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            if (!stream.CanRead)
            {
                throw new NotSupportedException();
            }

            using(var streamReader = new StreamReader(stream))
            {
                using(var jsonReader = new JsonTextReader(streamReader))
                {
                    var jsonResponse = new JsonSerializer();
                    var result = jsonResponse.Deserialize<T>(jsonReader);
                    return result;
                }
            }
        }
    }
}