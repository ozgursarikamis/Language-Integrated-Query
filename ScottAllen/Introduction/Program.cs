using System;
using System.Collections.Generic;
using System.IO;

namespace Introduction
{
    internal class Program
    {
        private static void Main()
        {
            const string path = @"C:\Windows";
            ShowLargeFilesWithoutLinq(path);
        }

        private static void ShowLargeFilesWithoutLinq(string path)
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            FileInfo[] files = directory.GetFiles();

            Array.Sort(files, new FileInfoComparer());

            foreach (var file in files)
            {
                Console.WriteLine($"{file.Name} - {file.Length}");
            }
        }
    }

    public class FileInfoComparer : IComparer<FileInfo>
    {
        public int Compare(FileInfo x, FileInfo y)
        {
            return y.Length.CompareTo(x.Length);
        }
    }
}
