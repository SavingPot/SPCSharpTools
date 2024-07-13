using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Tools
{
    public static class StringTools
    {
        public static void ModifySpecialPath(StringBuilder sb, string substitutionForEmpty, string substituteWord = "x")
        {
            sb.Replace(@"/", substituteWord);
            sb.Replace(@"\", substituteWord);
            sb.Replace(@"?", substituteWord);
            sb.Replace(@"*", substituteWord);
            sb.Replace(@":", substituteWord);
            sb.Replace("\"", substituteWord);
            sb.Replace(@"<", substituteWord);
            sb.Replace(@">", substituteWord);
            sb.Replace(@"|", substituteWord);

            var str = sb.ToString();
            switch (str)
            {
                case "aux":
                case "com1":
                case "com2":
                case "com3":
                case "com4":
                case "com5":
                case "com6":
                case "com7":
                case "com8":
                case "com9":
                case "prn":
                case "con":
                case "nul":
                    sb.Clear().Append(substitutionForEmpty);
                    break;

                default:
                    if (str.IsNullOrWhiteSpace())
                        sb.Clear().Append(substitutionForEmpty);
                    break;
            }
        }

        public static void ModifyObscenities(StringBuilder sb, string substitutionForEmpty, string substituteWord = "*")
        {
            sb.Replace("fucking", substituteWord);
            sb.Replace("fuck", substituteWord);
            sb.Replace("shit", substituteWord);
            sb.Replace("sb", substituteWord);
            sb.Replace("shabi", substituteWord);
            sb.Replace("cnm", substituteWord);
            sb.Replace("jb", substituteWord);
            sb.Replace("nt", substituteWord);
            sb.Replace("草泥马", substituteWord);
            sb.Replace("操你妈", substituteWord);
            sb.Replace("傻逼", substituteWord);
            sb.Replace("鸡巴", substituteWord);
            sb.Replace("贱", substituteWord);
            sb.Replace("去你妈", substituteWord);
            sb.Replace("勾巴", substituteWord);
            sb.Replace("脑瘫", substituteWord);
            sb.Replace("去死", substituteWord);

            if (sb.ToString().IsNullOrWhiteSpace())
                sb.Clear().Append(substitutionForEmpty);
        }
    }
}