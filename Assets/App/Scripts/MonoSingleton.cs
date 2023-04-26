using UnityEngine;

namespace App.Scripts
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();
                    if (_instance == null)
                    {
                        Debug.LogWarning("Accessing singleton of type " + typeof(T) + " before it has been created.");
                        var gameObject = new GameObject(typeof(T).Name);
                        _instance = gameObject.AddComponent<T>();
                    }
                }
                return _instance;
            }
        }
    }
}