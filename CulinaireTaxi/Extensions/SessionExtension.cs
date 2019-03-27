using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace CulinaireTaxi.Extensions
{

    /// <summary>
    /// Adds additional methods to ISession.
    /// </summary>
    public static class SessionExtension
    {

	/// <summary>
	/// Set the given key and value in this session.
	/// </summary>
	/// <param name="key">The key to set.</param>
	/// <param name="value">The value to set.</param>
	public static void SetObject(this ISession session, string key, object value)
	{
	    session.SetString(key, JsonConvert.SerializeObject(value));
	}

	/// <summary>
	/// Get the <typeparamref name="T"/> associated with the given key in this session.
	/// </summary>
	/// <typeparam name="T">The type of the object to get.</typeparam>
	/// <param name="key"></param>
	/// <returns>The <typeparamref name="T"/> associated with the given key, if any. Returns the default value of <typeparamref name="T"/> otherwise.</returns>
	public static T GetObject<T>(this ISession session, string key)
	{
	    string value = session.GetString(key);

	    return (value == null) ? default(T) : JsonConvert.DeserializeObject<T>(value);
	}

    }

}
