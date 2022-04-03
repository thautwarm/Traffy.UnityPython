// When using UnityPython in Unity with the builtin 'Unity' module
// you are responsible to present an root-level gameobejct attaching a 'UnityRTS' script
#if UNITY_VERSION
using UnityEngine;
#endif
namespace Traffy.Unity2D
{

#if UNITY_VERSION
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
#endif

}
