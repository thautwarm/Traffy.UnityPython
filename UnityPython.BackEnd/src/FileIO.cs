using System;
using System.Collections.Generic;
using System.Linq;
using Traffy.Objects;

namespace Traffy
{
    public static class IO
    {
        public static string GetAbsolutePath(string path)
        {
            return System.IO.Path.Combine(ModuleSystem.GetProjectDir(), path);
        }

        public static void ReadFile(List<byte> bytes, string path)
        {
            var abspath = GetAbsolutePath(path);
            var file = System.IO.File.Open(abspath, System.IO.FileMode.Open);
            // read bytes in 'file' to 'bytes'
            // buffer size is 1024
            if (!file.CanRead)
                throw new Traffy.Objects.RuntimeError($"Cannot read file '{abspath}'");
            var buffer = new byte[1024];
            var remain = file.Length;
            while (remain > 0)
            {
                var read = file.Read(buffer, 0, (int)Math.Min(remain, 1024));
                bytes.AddRange(buffer.Take(read));
                remain -= read;
            }
        }
        public static byte[] ReadFileBytes(string path)
        {
            var abspath = GetAbsolutePath(path);
            return System.IO.File.ReadAllBytes(abspath);
        }
        public static string ReadFile(string path, System.Text.Encoding encoding = null)
        {
            var abspath = GetAbsolutePath(path);
            return System.IO.File.ReadAllText(abspath, encoding ?? System.Text.Encoding.UTF8);
        }
        public static void WriteFile(string path, string content)
        {
            var abspath = GetAbsolutePath(path);
            System.IO.File.WriteAllText(abspath, content);
        }

        public static void WriteFile(string path, byte[] content)
        {
            var abspath = GetAbsolutePath(path);
            System.IO.File.WriteAllBytes(abspath, content);
        }
    }
}