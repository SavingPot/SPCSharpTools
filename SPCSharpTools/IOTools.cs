using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SP.Tools
{
    public static class IOTools
    {
        #region 文件夹
        public static void DeleteDir(string path) => new DirectoryInfo(path).Delete(true);

        public static void CopyDir(string sourceDirectoryName, string targetDirectoryName)
        {
            Directory.CreateDirectory(targetDirectoryName);

            DirectoryInfo source = new DirectoryInfo(sourceDirectoryName);
            DirectoryInfo target = new DirectoryInfo(targetDirectoryName);

            CopyDirWork(source, target);
        }

        private static void CopyDirWork(DirectoryInfo source, DirectoryInfo target)
        {
            foreach (DirectoryInfo dir in source.GetDirectories())
                CopyDirWork(dir, target.CreateSubdirectory(dir.Name));

            foreach (FileInfo file in source.GetFiles())
                file.CopyTo(Path.Combine(target.FullName, file.Name));
        }

        /// <summary>
        /// 移动文件夹中的所有文件夹与文件到另一个文件夹
        /// </summary>
        /// <param name="sourcePath">源文件夹</param>
        /// <param name="destPath">目标文件夹</param>
        public static void MoveFolder(string sourcePath, string destPath)
        {
            if (sourcePath.IsNullOrWhiteSpace())
                throw new ArgumentException($"调用 {nameof(MoveFolder)} 时需确认 {nameof(sourcePath)} 不为空");

            if (!Directory.Exists(sourcePath))
                throw new DirectoryNotFoundException($"调用 {nameof(MoveFolder)} 时需确认源目录 {nameof(sourcePath)} 存在");

            CreateDirectoryIfNone(destPath);

            //获得源文件下所有文件
            List<string> files = new List<string>(Directory.GetFiles(sourcePath));

            //移动所有文件
            files.ForEach(c => MoveFile(c, Path.Combine(new string[] { destPath, Path.GetFileName(c) })));

            //获得源文件下所有目录文件
            List<string> folders = new List<string>(Directory.GetDirectories(sourcePath));

            //采用递归的方法实现
            folders.ForEach(c => MoveFolder(c, Path.Combine(new string[] { destPath, Path.GetFileName(c) })));
        }

        public static void CreateDirectoryIfNone(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        public static void CreateDirsIfNone(params string[] paths)
        {
            for (int i = 0; i < paths.Length; i++)
            {
                CreateDirectoryIfNone(paths[i]);
            }
        }

        public static bool ExistDir(string path)
        {
            return Directory.Exists(path);
        }

        public static string GetDirectoryName(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            return dir.Name;

            //return Path.GetDirectoryName(path);
        }

        public static string[] GetFoldersInFolder(string path, bool returnPath = false)
        {
            string[] folders = Directory.GetDirectories(path);

            for (int i = 0; i < folders.Length; i++)
                folders[i] = folders[i].ToCSharpPath();

            //如果返回文件夹名称则获取文件夹名
            if (!returnPath)
                for (int i = 0; i < folders.Length; i++)
                    folders[i] = GetDirectoryName(folders[i]);

            return folders;
        }

        public static List<string> GetFoldersInFolderIncludingChildren(string path, bool returnPath = false)
        {
            List<string> paths = new List<string>();
            string[] folders = GetFoldersInFolder(path, true);

            for (int i = 0; i < folders.Length; i++)
            {
                paths.AddRange(GetFoldersInFolderIncludingChildrenWork(folders[i], returnPath));
            }

            return paths;
        }

        private static List<string> GetFoldersInFolderIncludingChildrenWork(string path, bool returnPath = false)
        {
            List<string> paths = new List<string>();
            string[] folders = GetFoldersInFolder(path, true);

            if (!returnPath)
                path = GetFileName(path);

            paths.Add(path);

            for (int i = 0; i < folders.Length; i++)
            {
                paths.AddRange(GetFoldersInFolderIncludingChildrenWork(folders[i], returnPath));
            }

            return paths;
        }
        #endregion



        #region 文件
        public static void MoveFile(string sourcePath, string destPath)
        {
            //覆盖模式
            if (File.Exists(destPath))
                File.Delete(destPath);

            File.Move(sourcePath, destPath);
        }

        public static bool ExistFile(string path)
        {
            return File.Exists(path);
        }

        public static StreamWriter CreateText(string path)
        {
            StreamWriter sw = File.CreateText(path);
            return sw;
        }

        public static StreamWriter CreateText(string path, string text)
        {
            StreamWriter sw = CreateText(path);
            sw.Write(text);
            return sw;
        }

        public static FileStream CreateFileIfNone(string path)
        {
            if (!File.Exists(path))
                return File.Create(path);

            return null;
        }

        public static void CreateFilesIfNone(params string[] paths)
        {
            for (int i = 0; i < paths.Length; i++)
            {
                CreateFileIfNone(paths[i]).Dispose();
            }
        }

        public static bool ExistAllFiles(params string[] paths)
        {
            for (int i = 0; i < paths.Length; i++)
            {
                if (!File.Exists(paths[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool ExistAnyFiles(params string[] paths)
        {
            for (int i = 0; i < paths.Length; i++)
            {
                if (File.Exists(paths[i]))
                {
                    return true;
                }
            }

            return false;
        }

        public static string GetFileName(string path, bool extension = true) => extension ? Path.GetFileName(path) : Path.GetFileNameWithoutExtension(path);

        public static byte[] FileToBytes(string filePath)
        {
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            byte[] bytes = new byte[(int)fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();
            return bytes;
        }

        public static void WriteBytesToFile(byte[] bytes, string targetPath)
        {
            FileStream fs = new FileStream(targetPath, FileMode.Create, FileAccess.Write);
            fs.Write(bytes,0,0);
            fs.Close();
        }

        public static string GetExtension(string path) => Path.GetExtension(path);

        public static byte[] LoadBytes(string path)
        {
            if (path.IsNullOrWhiteSpace())
                throw new FileNotFoundException($"{nameof(IOTools)}.{nameof(LoadBytes)}: 确保 path 不为空");

            using FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);

            if (fs == null)
                throw new FileNotFoundException($"{nameof(IOTools)}.{nameof(LoadBytes)}: 确保路径 {path} 正确");

            fs.Seek(0, SeekOrigin.Begin);
            byte[] bytes = new byte[fs.Length];
            fs.Read(bytes, 0, (int)fs.Length);
            fs.Close();

            return bytes;
        }

        public static string[] GetFilesInFolder(string path, bool returnPath = false, string extensionName = null)
        {
            string[] files = extensionName != null ? Directory.GetFiles(path, "*." + extensionName) : Directory.GetFiles(path);

            //将 \ 改为 /
            for (int i = 0; i < files.Length; i++)
                files[i] = files[i].ToCSharpPath();

            //如果返回文件名称则获取文件名
            if (!returnPath)
                for (int i = 0; i < files.Length; i++)
                    files[i] = GetFileName(files[i]);

            return files;
        }

        public static List<string> GetFilesInFolderIncludingChildren(string path, bool returnPath = false, string extensionName = null)
        {
            List<string> paths = new List<string>();
            string[] folders = GetFoldersInFolder(path, true);

            if (folders.Length > 0)
            {
                for (int i = 0; i < folders.Length; i++)
                {
                    paths.AddRange(GetFilesInFolderIncludingChildren(folders[i], returnPath, extensionName));
                }
            }

            paths.AddRange(GetFilesInFolder(path, returnPath, extensionName));

            return paths;
        }
        #endregion



        #region 目录
        public static string CombinePath(params string[] paths)
        {
            return Path.Combine(paths);
        }

        public static string GetParentPath(string path) => Directory.GetParent(path).FullName;

        public static string ToCSharpPath(string path) => path.Replace(@"\", "/");
        #endregion



        public static int GetTextSize(string str)
        {
            //使用Unicode编码的方式将字符串转换为字节数组,它将所有字符串(包括英文中文)全部以2个字节存储
            byte[] byteStr = Encoding.Unicode.GetBytes(str);
            int j = 0;

            for (int i = 0; i < byteStr.GetLength(0); i++)
            {
                //取余2是因为字节数组中所有的双数下标的元素都是unicode字符的第一个字节
                if (i % 2 == 0)
                    j++;
                //单数下标都是字符的第2个字节,如果一个字符第2个字节为0,则代表该Unicode字符是英文字符,否则为中文字符
                else if (byteStr[i] > 0)
                    j++;
            }

            return j;
        }
    }
}
