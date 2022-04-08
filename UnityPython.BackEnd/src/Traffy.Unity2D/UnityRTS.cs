// When using UnityPython in Unity with the builtin 'Unity' module
// you are responsible to present an root-level gameobejct attaching a 'UnityRTS' script
#if !NOT_UNITY
using System.Collections.Generic;
using Traffy.Objects;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Traffy.Unity2D
{
    public class UnityRTS : MonoBehaviour
    {
        public const float PixelPerUnit = 100;
        public string ProjectDirectory;
        public Camera MainCamera;
        public Canvas MainCanvas;
        public Dictionary<UnityEngine.Object, TrObject> allocations;
        public System.Func<Texture2D, Texture2D> API_RemoveBackground;
        public static UnityRTS Get;
        void Start()
        {
            
            Get = this;
            if (MainCamera == null)
                MainCamera = Camera.main;
            if (MainCanvas == null)
                MainCanvas = Object.FindObjectOfType<Canvas>();
            if (ProjectDirectory == null || ProjectDirectory.Trim().Length == 0)
                ProjectDirectory = Application.persistentDataPath;
            ModuleSystem.SetProjectDir(ProjectDirectory);
            allocations = new Dictionary<UnityEngine.Object, TrObject>();
            if (!MainCamera.TryGetComponent<Physics2DRaycaster>(out _))
            {
                MainCamera.gameObject.AddComponent<Physics2DRaycaster>();
            }
        }
    }
}
#endif
