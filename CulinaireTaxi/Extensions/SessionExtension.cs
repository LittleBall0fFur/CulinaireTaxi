using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace CulinaireTaxi.Extensions
{

    /// <summary>
    /// Adds additional methods to <see cref="ISession"/>.
    /// </summary>
    public static class SessionExtension
    {

        private static readonly JsonSerializerSettings SERIALIZER_SETTINGS = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };

        /// <summary>
        /// Set the given key and value in this session.
        /// </summary>
        /// <param name="key">The key to set.</param>
        /// <param name="value">The value to set.</param>
        public static void SetObject(this ISession @this, string key, object value)
        {
            @this.SetString(key, JsonConvert.SerializeObject(value, SERIALIZER_SETTINGS));
        }

        /// <summary>
        /// Get the <typeparamref name="T"/> associated with the given key in this session.
        /// </summary>
        /// <typeparam name="T">The type of the object to get.</typeparam>
        /// <param name="key"></param>
        /// <returns>The <typeparamref name="T"/> associated with the given key, if any. Returns the default value of <typeparamref name="T"/> otherwise.</returns>
        public static T GetObject<T>(this ISession @this, string key)
        {
            string value = @this.GetString(key);

            return (value == null) ? default(T) : JsonConvert.DeserializeObject<T>(value, SERIALIZER_SETTINGS);
        }

    }

}
