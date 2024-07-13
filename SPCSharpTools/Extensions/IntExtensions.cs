using System;

namespace SP.Tools
{
    public static class IntExtensions
	{
		public static int IAbs(this int f) => Math.Abs(f);

		public static bool IIsPositive(this int f) => f.ISign() == 1;

		public static bool IIsNegative(this int f) => f.ISign() == -1;

		[ChineseName("I符号")] public static int ISign(this int f) => Math.Sign(f);

		public static float ToSecond(this int milliSecondNum) => milliSecondNum / 1000;

		public static ushort ToUShort(this int num)
        {
			int numTemp = num;

			if (numTemp > ushort.MaxValue)
				numTemp = ushort.MaxValue;
			else if (numTemp < ushort.MinValue)
				numTemp = ushort.MinValue;

			return Convert.ToUInt16(numTemp);
		}

        public static bool IInRange(this int f, float min, float max)
        {
            if (f < min || f > max)
                return false;

            return true;
        }
    }
}
