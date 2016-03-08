using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.IO.Compression;	// with Microsoft Compression Nuget Libary

namespace XmJose.Json
{
	public class Deflate
	{
		public static byte[] Compress(object data)
		{
			using (var writer = new StreamWriter(new MemoryStream()))
			{
				writer.Write(data);
				writer.Flush();
				writer.BaseStream.Position = 0;

				return Compress(writer.BaseStream);
			}
		}

		public static byte[] CompressBytes(byte[] data)
		{

			using (var mem = new MemoryStream())
			{              
				mem.Write(data, 0, data.Length);
				mem.Flush();
				mem.Position = 0;
				return Compress(mem);
			}
		}

		public static byte[] Compress(Stream input)
		{
			using (var compressStream = new MemoryStream())
			using (var compressor = new DeflateStream(
				compressStream, CompressionMode.Compress))
			{
				input.CopyTo(compressor);
				compressor.Flush ();
				return compressStream.ToArray();
			}
		}

		public static Stream DecompressToStream(byte[] input)
		{
			var output = new MemoryStream();

			using (var compressStream = new MemoryStream(input))
			using (var decompressor = new DeflateStream(
				compressStream, CompressionMode.Decompress))
				decompressor.CopyTo(output);

			output.Position = 0;
			return output;
		}
		public static byte[] DecompressToBytes(byte[] input)
		{

			using (var memoryStream = new MemoryStream())
			{
				DecompressToStream(input).CopyTo(memoryStream);
				return memoryStream.ToArray();
			}
		}
		public static string  DecompressToString(byte[] input)
		{
			using (var reader = new StreamReader(
				DecompressToStream(input)
			))
			{
				return reader.ReadToEnd();

			}
		}
	}
}