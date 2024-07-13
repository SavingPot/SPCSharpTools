using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Tools
{
    public static class FloatExtensions
    {
        public static float Abs(this float f) => Math.Abs(f);

        public static bool IsPositive(this float f) => f.Sign() == 1;

        public static bool IsNegative(this float f) => f.Sign() == -1;

        [ChineseName("符号")] public static float Sign(this float f) => Math.Sign(f);

        [ChineseName("转换为 -1 0 1")]
        public static float ToNOneZeroAndOne(this float f, float value = 0.2f)
        {
            if (f <= -value) f = -1;
            if (f >= value) f = 1;
            if (f > -value && f < value) f = 0;
            return f;
        }

        [ChineseName("转换为 -1 1")]
        public static float ToNOneAndOne(this float f, float value = 0.2f)
        {
            if (f <= -value) f = -1;
            if (f >= value) f = 1;
            return f;
        }

        public static int ToMilliSecond(this float secondNum) => Convert.ToInt32(secondNum * 1000);

        public static bool InRange(this float f, float min, float max)
        {
            if (f < min || f > max)
                return false;

            return true;
        }
    }
}
