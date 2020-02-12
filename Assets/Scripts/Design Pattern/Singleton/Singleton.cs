using UnityEngine;


    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if(_instance==null)
                    Debug.Log(typeof(T).ToString()+" is NULL");
                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance==null)
            {
                _instance = this as T;
            }
            else
            {
                if (_instance!=this)
                {
                    Destroy(gameObject);
                }
            }
            Init();
        }

        public virtual void Init()
        {
        
        }
    }

