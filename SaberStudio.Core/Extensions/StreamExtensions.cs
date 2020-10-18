using Newtonsoft.Json;
using System;
using System.IO;

namespace SaberStudio.Core.Extensions
{
    public static class StreamExtensions
    {
        public static T DeserializeJsonFromStream<T>(this Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            if (!stream.CanRead)
                throw new NotSupportedException("Cannot read from unsupported stream");

            using (var streamReader = new StreamReader(stream))
            using (var jsonTextReader = new JsonTextReader(streamReader))
            {
                var jsonSerializer = new JsonSerializer();
                return jsonSerializer.Deserialize<T>(jsonTextReader);
            }
        }
    }
}
