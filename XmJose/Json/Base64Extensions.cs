using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Org.BouncyCastle.Math;


namespace XmJose.Json
{
	public static class Base64Extensions
	{
		public static string ToBase64Url(this byte[] bytes)
		{
			StringBuilder result = new StringBuilder(
				Convert.ToBase64String(bytes).TrimEnd('='));
			result.Replace('+', '-');
			result.Replace('/', '_');
			return result.ToString();
		}
		public static byte[] ToBytesFromBase64Url(this string base64ForUrlInput)
		{
			int padChars = (base64ForUrlInput.Length % 4) == 0 ? 0 : (4 - (base64ForUrlInput.Length % 4));
			StringBuilder result = new StringBuilder(base64ForUrlInput, base64ForUrlInput.Length + padChars);
			result.Append(String.Empty.PadRight(padChars, '='));
			result.Replace('-', '+');
			result.Replace('_', '/');
			return Convert.FromBase64String(result.ToString());
		}

		#region BigInteger
		public static string ToBase64Url(this BigInteger bg)
		{
			var bytes = bg.ToByteArrayUnsigned ();
			return bytes.ToBase64Url ();
		}

		public static BigInteger ToBigIntegerFromBase64url(this string src)
		{
			int UNSIGNED = 1;

			return new BigInteger(UNSIGNED, src.ToBytesFromBase64Url());
		}
		#endregion


		#region string
		public static string ToBase64Url(this string src)
		{
			return src.ToByteArray().ToBase64Url();
		}

		public static string ToStringFromBase64Url(this string base64url)
		{
			var bytes = base64url.ToBytesFromBase64Url ();
			return Encoding.UTF8.GetString(bytes, 0, bytes.Length);
		}
		#endregion


		#region chars
		public static char[] ToCharsFromBase64Url(this string base64url)
		{
			return Encoding.UTF8.GetChars(
				base64url.ToBytesFromBase64Url()
			);
		}
		#endregion
	}
}

