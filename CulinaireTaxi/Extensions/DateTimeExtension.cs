using System;

namespace CulinaireTaxi.Extensions
{

    public static class DateTimeExtension
    {

	public static string ToMySqlFormat(this DateTime @this)
	{
	    return @this.ToString("yyyy-MM-dd HH:mm:ss");
	}

    }

}
