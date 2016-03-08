using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace XmJose.Json
{
	public static class StreamExtensions
	{
		public static readonly int BUFFER_SIZE = 4096 * 8; 

		/// <summary>
		/// Compress with Microsoft Compression
		/// </summary>
		/// <param name="src">source data in byte array</param>
		/// <returns>compressed data in byte array</returns>
		public static byte[] ToCompressed(this byte[] src)
		{
			return Deflate.CompressBytes(src);
		}
		/// <summary>
		/// Uncompress with Microsoft COmpression
		/// </summary>
		/// <param name="src">Compressed byte array</param>
		/// <returns>Original data in byte array</returns>
		public static byte[] ToDecompressed(this byte[] src)
		{
			return Deflate.DecompressToBytes(src);
		}

		public static byte[] ToByteArray(this Stream stream)
		{
			return stream.ToByteArray ();
		}
		public static byte[] ToByteArray(this string str)
		{
			return System.Text.Encoding.UTF8.GetBytes(str);
		}
		public static char[] ToCharArray(this string str)
		{
			return null;

		}
		public static string ToUtf8(this byte[] octets)
		{
			return System.Text.Encoding.UTF8.GetString(octets,0, octets.Length);
		}

		/// <summary>
		/// Convert 64 bit ulong integer into byte array
		/// </summary>
		/// <param name="value">ulong integer</param>
		/// <returns>byte[]</returns>
		public static byte[] ToOctetString(this ulong value)
		{
			var ret = BitConverter.GetBytes(value);

			if (BitConverter.IsLittleEndian)
				Array.Reverse(ret);

			return ret;
		}

		/// <summary>
		/// Convert 32 bit uint into byte array
		/// </summary>
		/// <param name="value">uint integer</param>
		/// <returns>byte[]</returns>
		public static byte[] ToOctetString(this uint value)
		{
			var ret = BitConverter.GetBytes(value);
			if (BitConverter.IsLittleEndian)
				Array.Reverse(ret);

			return ret;
		}

		public static ulong ToUint32(this byte[] seqence)
		{
			if (BitConverter.IsLittleEndian)
				Array.Reverse (seqence);
			return BitConverter.ToUInt32 (seqence, 0);
		}

		public static byte[] Slice(this byte[] org,int start, int end = int.MaxValue)
		{
			if (end < 0)
			{
				end = org.Length + end;
			}
			start = Math.Max(0, start);
			end = Math.Max(start, end);

			return org.Skip(start).Take(end - start).ToArray();

		}

		public static byte[] Slice(this byte[] org, uint start, uint end = int.MaxValue)
		{
			return org.Slice((int)start, (int)end);

		}
		public static Dictionary<string, string> ParseQuery(this string query)
		{
			return query
				.Split ("?&".ToCharArray ())
				.Where (i => string.IsNullOrEmpty (i) == false)
				.Select (i => i.Split ('='))
				.ToDictionary (
					i => Uri.UnescapeDataString (i [0]), 
					i => Uri.UnescapeDataString (i [1]));
		}
	}
}

