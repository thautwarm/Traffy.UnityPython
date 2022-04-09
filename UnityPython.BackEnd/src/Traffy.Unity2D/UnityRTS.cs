// When using UnityPython in Unity with the builtin 'Unity' module
// you are responsible to present an root-level gameobejct attaching a 'UnityRTS' script
#if !NOT_UNITY
using System.Collections.Generic;
using System.Linq;
using Traffy.Objects;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Traffy.Unity2D
{
    public class UnityRTS : MonoBehaviour
    {
        public const float PixelPerUnit = 100;
        public string ProjectDirectory;
        public string MainModule = "main";
        public Camera MainCamera;
        private bool IsInitialized = false;
        
        [HideInInspector]
        public Dictionary<UnityEngine.Object, TrObject> allocations;
        public static UnityRTS Get;
        public void ReSetting()
        {
            Get = this;
            if (MainCamera == null)
                MainCamera = Camera.main;
            if (ProjectDirectory == null || ProjectDirectory.Trim().Length == 0)
                ProjectDirectory = Application.persistentDataPath;
            ModuleSystem.SetProjectDir(ProjectDirectory);
            if (!MainCamera.TryGetComponent<Physics2DRaycaster>(out _))
            {
                MainCamera.gameObject.AddComponent<Physics2DRaycaster>();
            }
        }
        public void ReloadPython()
        {
            if (allocations != null)
                foreach(var kv in allocations)
                {
                    UnityEngine.Object.Destroy(kv.Key);
                }
            allocations = new Dictionary<UnityEngine.Object, TrObject>();
#if !CODE_GEN
            Initialization.InitRuntime();
#endif
            var CompiledDirectory = System.IO.Path.Combine(ProjectDirectory, "Compiled");
            if (!System.IO.Directory.Exists(CompiledDirectory))
            {
                throw new System.ArgumentException($"Python compiled directory ({CompiledDirectory}) not found");
            }
            ModuleSystem.LoadDirectory(CompiledDirectory);
            if (!ModuleSystem.Modules.ContainsKey(MainModule))
            {
                throw new System.ArgumentException($"Python entry point module ({MainModule}) not found");
            }
            ModuleSystem.ImportModule(MainModule);
        }
        public void Init()
        {
            if (!IsInitialized)
            {
                ReSetting();
                ReloadPython();
                IsInitialized = true;
            }
        }
    }
}
#endif
