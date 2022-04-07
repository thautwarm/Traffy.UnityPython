// When using UnityPython in Unity with the builtin 'Unity' module
// you are responsible to present an root-level gameobejct attaching a 'UnityRTS' script
#if !NOT_UNITY
using UnityEngine;

namespace Traffy.Unity2D
{
    public class UnityRTS : MonoBehaviour
    {
        public string ProjectDirectory;
        public Camera MainCamera;
        public Canvas MainCanvas;
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
        }
    }
}
#endif
