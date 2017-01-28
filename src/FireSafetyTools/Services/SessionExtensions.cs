using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace FireSafetyTools.Services
{
    public static class SessionExtensions
    {
        

        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            // This setting is necessary to correct serialize of derived classes
            JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };

            var serialize = JsonConvert.SerializeObject(value, settings);

            session.SetString(key, serialize);
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            // This setting is necessary to correct deserialize of derived classes
            JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };

            var value = session.GetString(key);

            if (value == null)
            {
                return default(T);
            }

            var deserialize = JsonConvert.DeserializeObject<T>(value, settings);

            return deserialize;
        }
    }
}
