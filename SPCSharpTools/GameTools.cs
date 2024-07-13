using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Tools
{
    public static class GameTools
    {
        public static bool CompareVersions(string a, string b, Operators operatorType = Operators.than)
        {
            if (a.IsNullOrWhiteSpace() || b.IsNullOrWhiteSpace())
            {
                throw new ArgumentException($"{nameof(GameTools)}.{nameof(CompareVersions)}: 版本号不能为空");
            }

            var aSplitted = a.Split('.');
            var bSplitted = b.Split('.');

            if (aSplitted.Length != 3 || bSplitted.Length != 3)
            {
                throw new ArgumentException($"{nameof(GameTools)}.{nameof(CompareVersions)}: 版本号格式不规范");
            }

            switch (operatorType)
            {
                case Operators.equal:
                    return a == b;

                case Operators.than:
                    for (int i = 0; i < 3; i++)
                    {
                        if (aSplitted[i].ToInt() < bSplitted[i].ToInt())
                            return false;

                        if (aSplitted[i].ToInt() > bSplitted[i].ToInt())
                            return true;
                    }

                    break;

                case Operators.thanOrEqual:
                    if (a == b) return true;

                    for (int i = 0; i < 3; i++)
                    {
                        if (aSplitted[i].ToInt() < bSplitted[i].ToInt())
                            return false;

                        if (aSplitted[i].ToInt() > bSplitted[i].ToInt())
                            return true;
                    }

                    break;
                case Operators.less:
                    for (int i = 0; i < 3; i++)
                    {
                        if (aSplitted[i].ToInt() > bSplitted[i].ToInt())
                            return false;

                        if (aSplitted[i].ToInt() < bSplitted[i].ToInt())
                            return true;
                    }

                    break;
                case Operators.lessOrEqual:
                    if (a == b) return true;

                    for (int i = 0; i < 3; i++)
                    {
                        if (aSplitted[i].ToInt() > bSplitted[i].ToInt())
                            return false;

                        if (aSplitted[i].ToInt() < bSplitted[i].ToInt())
                            return true;
                    }

                    break;
            }

            return false;
        }

        public static string GetHighestVersion(string versionsPath)
        {
            if (!Directory.Exists(versionsPath))
                throw new Exception();

            string[] paths = IOTools.GetFoldersInFolder(versionsPath, true);
            string high = string.Empty;

            for (int i = 0; i < paths.Length; i++)
            {
                string path = paths[i];
                string dirName = IOTools.GetDirectoryName(path);

                if (i == 0)
                {
                    high = path;
                }
                else
                {
                    try
                    {
                        if (GameTools.CompareVersions(dirName, IOTools.GetDirectoryName(high)))
                        {
                            high = path;
                        }
                    }
                    catch
                    {

                    }
                }
            }

            return high;
        }
    }

    public enum Operators
    {
        equal,
        than,
        thanOrEqual,
        less,
        lessOrEqual
    }
}
