using System;
using System.Collections.Generic;
using Traffy.Objects;
using System.Linq;

namespace Traffy
{
    public class ModuleSystem
    {
        static object ModuleLock = new object();
        static Dictionary<string, TrModule> modules = new Dictionary<string, TrModule>();

        static string PROJECT_DIR = ".";
        public static void SetProjectDir(string dir)
        {
            PROJECT_DIR = dir;
        }
        public static string GetProjectDir()
        {
            return PROJECT_DIR;
        }

        public static bool ContainModule(string name)
        {
            return modules.ContainsKey(name);
        }

        // load to the global modules, but not executed
        public static void DynamicLoad(string name, string path)
        {
            if (modules.ContainsKey(name))
            {
                throw new InvalidProgramException($"Module '{name}' already exists");
            }
            path = System.IO.Path.Combine(PROJECT_DIR, path);
            var sourceCode = System.IO.File.ReadAllText(path);
            var spec = ModuleSpec.Parse(sourceCode);
            var mod = TrModule.CreateUninitialized(name, spec);
            lock (ModuleLock)
            {
                modules[name] = mod;
            }
        }

        public static TrModule ImportModule(string name)
        {
            if (modules.TryGetValue(name, out var mod))
            {
                mod.InitInst();
                return mod;
            }
            if (modules.TryGetValue(name + ".__init__", out mod))
            {
                mod.InitInst();
                return mod;
            }
            throw new ImportError(name, MK.None(), $"No module '{name}'");
        }
    }
}