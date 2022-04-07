// When using UnityPython in Unity with the builtin 'Unity' module
// you are responsible to present an root-level gameobejct attaching a 'UnityRTS' script
#if !NOT_UNITY
using UnityEngine;

namespace Traffy.Unity2D
{
    public class UnityRTS : MonoBehaviour
    {
        public string ProjectDirectory = Application.persistentDataPath;
        public Camera MainCamera = Camera.main;
        public Canvas MainCanvas = Camera.main.GetComponent<Canvas>();
        public static UnityRTS Get;

        void Awake()
        {
            if (!Get)
            {
                Get = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
        }
    }
}
#endif
