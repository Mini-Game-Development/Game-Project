using UnityEngine;
//using UnityGameFramework.Runtime;

namespace Game
{
    /// <summary>
    /// 需要使用Unity生命周期的单例模式
    /// 使用Awake跟OnDestroy要注意override的問題
    /// </summary>
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        protected abstract bool _dontDestroyOnLoad { get; }

        protected virtual void Awake()
        {
            if (_dontDestroyOnLoad)
                DontDestroyOnLoad(gameObject);
        }

        protected static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();
                    if(FindObjectsOfType<T>().Length > 1)
                    {
                        //Log.Error("More than 1 !! [{0}]", _instance.gameObject.name);
                        return _instance;
                    }

                    if(_instance == null)
                    {
                        string name = typeof(T).Name;
                       // Log.Info("Instance Name : " + name);

                        GameObject instanceGO = GameObject.Find(name);
                        if(instanceGO == null)
                        {
                            instanceGO = new GameObject(name);
                        }
                        _instance = instanceGO.AddComponent<T>();

                        //Log.Info("Add New Singleton " + _instance.name + " in Game");

                        if (!Application.isPlaying)
                            Debug.LogError($"{name} spawn on editor mode!!");

                    }
                }
                return _instance;
            }
        }

        public static bool IsCreate => _instance != null;
        
        protected virtual void OnDestroy()
        {
            _instance = null;
        }
    }
}