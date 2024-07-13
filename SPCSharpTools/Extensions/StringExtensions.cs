using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Tools
{
    public static class StringExtensions
    {
        public static string ToCSharpPath(this string path) => IOTools.ToCSharpPath(path);

        public static int ToInt(this string str) => int.TryParse(str, out int temp) ? temp : 0;

        public static float ToFloat(this string str) => float.TryParse(str, out float temp) ? temp : 0;

        public static long ToLong(this string str) => long.TryParse(str, out long temp) ? temp : 0;

        public static bool IsNullOrEmpty(this string str) => string.IsNullOrEmpty(str);

        public static bool IsNullOrWhiteSpace(this string str) => string.IsNullOrWhiteSpace(str);

        public static bool BelongsToInterval(this float value, string interval)
        {
            var startBrackets = interval[0];
            var endBrackets = interval[interval.Length - 1];

            var splitted = interval.Remove(interval.Length - 1, 1).Remove(0, 1).Split(',');
            var startValue = splitted[0].ToFloat();
            var endValue = splitted[1].ToFloat();

            return (startBrackets, endBrackets) switch
            {
                ('(', ')') => value > startValue && value < endValue,
                ('[', ')') => value >= startValue && value < endValue,
                ('(', ']') => value > startValue && value <= endValue,
                ('[', ']') => value >= startValue && value <= endValue,
                _ => throw new(),
            };
        }

        public static bool BelongsToFormulaInterval(this float value, string interval)
        {
            var startBrackets = interval[0];
            var endBrackets = interval[interval.Length - 1];

            var splitted = interval.Remove(interval.Length - 1, 1).Remove(0, 1).Split(',');
            var startValue = ComputeFormula(splitted[0]);
            var endValue = ComputeFormula(splitted[1]);

            return (startBrackets, endBrackets) switch
            {
                ('(', ')') => value > startValue && value < endValue,
                ('[', ')') => value >= startValue && value < endValue,
                ('(', ']') => value > startValue && value <= endValue,
                ('[', ']') => value >= startValue && value <= endValue,
                _ => throw new(),
            };
        }

        public static bool BelongsToFormulaInterval(this float value, string interval, FormulaAlgebra algebra)
        {
            var startBrackets = interval[0];
            var endBrackets = interval[interval.Length - 1];

            var splitted = interval.Remove(interval.Length - 1, 1).Remove(0, 1).Split(',');
            var startValue = ComputeFormula(splitted[0], algebra);
            var endValue = ComputeFormula(splitted[1], algebra);

            return (startBrackets, endBrackets) switch
            {
                ('(', ')') => value > startValue && value < endValue,
                ('[', ')') => value >= startValue && value < endValue,
                ('(', ']') => value > startValue && value <= endValue,
                ('[', ']') => value >= startValue && value <= endValue,
                _ => throw new(),
            };
        }

        public static void SplitIntervalFormulaIntoMinMaxFormula(this string intervalFormula, out string minFormula, out string maxFormula)
        {
            var splitted = intervalFormula.Split(new[] { "=>" }, StringSplitOptions.None);

            if (splitted.Length == 1)
            {
                minFormula = splitted[0];
                maxFormula = minFormula;
            }
            else if (splitted.Length == 2)
            {
                minFormula = splitted[0];
                maxFormula = splitted[1];
            }
            else
            {
                throw new();
            }
        }

        public static float ComputeFormula(this string formula)
        {
            //计算表达式
            DataTable dt = new();
            object result = dt.Compute(formula, "false");

            return float.Parse(result.ToString());
        }

        public static float ComputeFormula(this string formula, FormulaAlgebra algebra)
        {
            //把指定规则替换为数字
            for (int i = 0; i < algebra.Count; i++)
            {
                var ele = algebra.ElementAt(i);
                formula = formula.Replace(ele.Key, ele.Value.ToString());
            }

            //计算表达式并返回
            return ComputeFormula(formula);
        }

        public static bool TryComputeFormula(this string formula, out float num)
        {
            //计算表达式
            DataTable dt = new();

            try
            {
                object result = dt.Compute(formula, "false");

                num = float.Parse(result.ToString());

                return true;
            }
            catch
            {
                num = 0;
                return false;
            }
        }

        public static bool TryComputeFormula(this string formula, FormulaAlgebra algebra, out float num)
        {
            try
            {
                //把指定规则替换为数字
                for (int i = 0; i < algebra.Count; i++)
                {
                    var ele = algebra.ElementAt(i);
                    formula = formula.Replace(ele.Key, ele.Value.ToString());
                }

                //计算表达式并返回
                num = ComputeFormula(formula);
                return true;
            }
            catch
            {
                num = 0;
                return false;
            }
        }

        public static T ToEnum<T>(this string str) where T : struct
        {
            return (T)Enum.Parse(typeof(T), str);
        }

        public static string[] Lines(this string source)
        {
            return source.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
        }
    }

    public class FormulaAlgebra : Dictionary<string, float>
    {
        
    }
}
