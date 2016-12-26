using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace FireSafetyTools.Services
{
    public static class SessionExtensions
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            var serialize = JsonConvert.SerializeObject(value);

            session.SetString(key, serialize);
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);

            if (value == null)
            {
                return default(T);
            }

            var deserialize = JsonConvert.DeserializeObject<T>(value);

            return deserialize;
        }
    }
}
