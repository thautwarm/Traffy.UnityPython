using System;
using System.Collections.Generic;
using Traffy.Objects;
using System.Linq;

namespace Traffy
{
    public class ModuleSystem
    {
        public static void LoadDirectory(string directory)
        {
            var files = System.IO.Directory.GetFiles(directory, "*" + Initialization.IR_FILE_SUFFIX);
            foreach (var file in files)
            {
                var sourceCode = System.IO.File.ReadAllText(file);
                var spec = ModuleSpec.Parse(sourceCode);
                DynamicLoadSpec(spec);
            }
        }

        static object ModuleLock = new object();
        static Dictionary<string, TrObject> modules;
        static string PROJECT_DIR;
        public static Dictionary<string, TrObject> Modules => modules;

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            modules = new Dictionary<string, TrObject>();
            PROJECT_DIR = ".";

        }
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
        public static void DynamicLoadSpec(string name, string path)
        {
            if (modules.ContainsKey(name))
            {
                throw new InvalidProgramException($"Module '{name}' already exists");
            }
            DynamicLoadSpec(path);
        }

        public static void DynamicLoadSpec(string path)
        {
            var sourceCode = System.IO.File.ReadAllText(path);
            var spec = ModuleSpec.Parse(sourceCode);
            DynamicLoadSpec(spec);
        }

        public static void DynamicLoadSpec(ModuleSpec spec)
        {
            if (modules.ContainsKey(spec.modulename))
            {
                throw new InvalidProgramException($"Module '{spec.modulename}' already exists");
            }
            var mod = TrModule.CreateUninitialized(spec.modulename, spec);
            lock (ModuleLock)
            {
                modules[spec.modulename] = mod;
            }
        }


        public static TrObject ImportModule(string name)
        {
            if (modules.TryGetValue(name, out var mod))
            {
                if (mod is TrModule regularModule)
                {
                    regularModule.InitInst();
                }
                
                return mod;
            }
            if (modules.TryGetValue(name + ".__init__", out mod))
            {
                if (mod is TrModule regularModule)
                {
                    regularModule.InitInst();
                }
                return mod;
            }
            throw new ImportError(name, MK.None(), $"No module '{name}'");
        }
    }
}