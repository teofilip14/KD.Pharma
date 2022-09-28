using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KD.WPF.Client.APIClient
{
    public static class JsonConvertWrapper
    {
        public static bool TryDeserializeObject<T>(string value, out T result)
        {
            result = default(T);
            try
            {
                result = JsonConvert.DeserializeObject<T>(value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static T DeserializeObject<T>(string value, JsonConverter[] converters)
        {
            return JsonConvert.DeserializeObject<T>(value, converters);
        }

        public static string SerializeObject(object value)
        {
            return JsonConvert.SerializeObject(value, Formatting.Indented);
        }

        public static string SerializeObjectWithUTC(object value)
        {
            return JsonConvert.SerializeObject(value, Formatting.Indented, new JsonSerializerSettings
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Utc
            });
        }

        public static T DeserializeObject<T>(string value, JsonSerializerSettings? settings = null)
        {
            return JsonConvert.DeserializeObject<T>(value, settings);
        }
    }
}
