using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SP.Tools
{
    public static class SystemByteConvert
    {
        public static byte[] ToBytes(bool value) => BitConverter.GetBytes(value);

        public static byte[] ToBytes(int value) => BitConverter.GetBytes(value);

        public static byte[] ToBytes(uint value) => BitConverter.GetBytes(value);

        public static byte[] ToBytes(float value) => BitConverter.GetBytes(value);

        public static byte[] ToBytes(double value) => BitConverter.GetBytes(value);

        public static byte[] ToBytes(short value) => BitConverter.GetBytes(value);

        public static byte[] ToBytes(ushort value) => BitConverter.GetBytes(value);

        public static byte[] ToBytes(long value) => BitConverter.GetBytes(value);

        public static byte[] ToBytes(ulong value) => BitConverter.GetBytes(value);

        public static byte[] ToBytes(string value) => Encoding.UTF8.GetBytes(value);

        public static byte[] ToBytes(char value) => BitConverter.GetBytes(value);



        public static bool ToBool(byte[] bytes) => BitConverter.ToBoolean(bytes, 0);

        public static int ToInt(byte[] bytes) => BitConverter.ToInt32(bytes, 0);

        public static uint ToUInt(byte[] bytes) => BitConverter.ToUInt32(bytes, 0);

        public static float ToFloat(byte[] bytes) => BitConverter.ToSingle(bytes, 0);

        public static double ToDouble(byte[] bytes) => BitConverter.ToDouble(bytes, 0);

        public static short ToShort(byte[] bytes) => BitConverter.ToInt16(bytes, 0);

        public static ushort ToUShort(byte[] bytes) => BitConverter.ToUInt16(bytes, 0);

        public static long ToLong(byte[] bytes) => BitConverter.ToInt64(bytes, 0);

        public static ulong ToULong(byte[] bytes) => BitConverter.ToUInt64(bytes, 0);

        public static string ToString(byte[] bytes) => Encoding.UTF8.GetString(bytes);

        public static char ToChar(byte[] bytes) => BitConverter.ToChar(bytes, 0);

        public static MemoryStream ToMemoryStream(byte[] bytes) => new MemoryStream(bytes);
    }
}
