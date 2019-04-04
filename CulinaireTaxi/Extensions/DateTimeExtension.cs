using System;

namespace CulinaireTaxi.Extensions
{

    /// <summary>
    /// Adds additional methods to <see cref="DateTime"/>.
    /// </summary>
    public static class DateTimeExtension
    {

	/// <summary>
	/// Converts the value of the current <see cref="DateTime"/> object to a string in MySql compatible format.
	/// </summary>
	/// <returns>The current <see cref="DateTime"/> as a string in MySql compatible format.</returns>
	public static string ToMySqlFormat(this DateTime @this)
	{
	    return @this.ToString("yyyy-MM-dd HH:mm:ss");
	}

    }

}
