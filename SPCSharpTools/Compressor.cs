using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Tools
{
	public static class Compressor
	{
		/// <summary>
		/// 返回传入字符串以 GZip 压缩算法压缩后的 Base64编码字符
		/// </summary>
		/// <param name="rawString">需要压缩的字符串</param>
		/// <returns>压缩后的Base64编码的字符串</returns>
		public static string[] CompressStrings(string[] rawString)
		{
			List<string> vs = new List<string>();

			foreach (var v in rawString)
			{
				vs.Add(CompressString(v));
			}

			return vs.ToArray();
		}

		/// <summary>
		/// 将传入的二进制字符串以 GZip 算法解压缩
		/// </summary>
		/// <param name="zippedString">经GZip压缩后的二进制字符串</param>
		/// <returns>原始未压缩字符串</returns>
		public static string[] DecompressStrings(string[] zippedStrings)
		{
			List<string> vs = new List<string>();

			foreach (var v in zippedStrings)
			{
				vs.Add(DecompressString(v));
			}

			return vs.ToArray();
		}

		/// <summary>
		/// 返回传入字符串以 GZip 压缩算法压缩后的 Base64编码字符
		/// </summary>
		/// <param name="rawString">需要压缩的字符串</param>
		/// <returns>压缩后的Base64编码的字符串</returns>
		public static string CompressString(string rawString)
		{
			if (string.IsNullOrEmpty(rawString) || rawString.Length == 0)
				return "";
			else
			{
				//把 string 转为 byte[]
				byte[] rawData = SystemByteConvert.ToBytes(rawString.ToString());
				//用 gzip 压缩转换后的字符串
				byte[] zippedData = CompressBytes(rawData);
				return Convert.ToBase64String(zippedData);
			}
		}

		/// <summary>
		/// 通过 GZip 压缩数据
		/// </summary>
		/// <param name="rawData"></param>
		/// <returns></returns>
		public static byte[] CompressBytes(byte[] rawData)
		{
			MemoryStream ms = new MemoryStream();
			GZipStream compressedZipStream = new GZipStream(ms, CompressionMode.Compress, true);
			compressedZipStream.Write(rawData, 0, rawData.Length);
			compressedZipStream.Close();
			return ms.ToArray();
		}


		/// <summary>
		/// 将传入的二进制字符串以 GZip 算法解压缩
		/// </summary>
		/// <param name="zippedString">经GZip压缩后的二进制字符串</param>
		/// <returns>原始未压缩字符串</returns>
		public static string DecompressString(string zippedString)
		{
			if (string.IsNullOrEmpty(zippedString) || zippedString.Length == 0)
				return "";
			else
			{
				byte[] zippedData = Convert.FromBase64String(zippedString.ToString());
				return Encoding.UTF8.GetString(DecompressBytes(zippedData));
			}
		}

		/// <summary>
		/// Gzip 算法解压
		/// </summary>
		/// <param name="zippedData"></param>
		/// <returns></returns>
		public static byte[] DecompressBytes(byte[] zippedData)
		{
			MemoryStream ms = new MemoryStream(zippedData);
			GZipStream compressedZipStream = new GZipStream(ms, CompressionMode.Decompress);
			MemoryStream outBuffer = new MemoryStream();
			byte[] block = new byte[1024];
			while (true)
			{
				int bytesRead = compressedZipStream.Read(block, 0, block.Length);
				if (bytesRead <= 0)
					break;
				else
					outBuffer.Write(block, 0, bytesRead);
			}
			compressedZipStream.Close();
			return outBuffer.ToArray();
		}
	}
}
