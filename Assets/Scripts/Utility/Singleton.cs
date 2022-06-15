using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public bool isPersistant = true;
    private static T _instance;

    public static T instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindOrCreateInstance(); 
            }
           

            return _instance;
        }
    }

    public virtual void Awake()
    {
        if (isPersistant)
        {
            if (!_instance)
            {
                _instance = this as T;
            }
            else
            {
                Object.Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            _instance = this as T;
        }
    }


    static T FindOrCreateInstance()
    {
        var instance = GameObject.FindObjectOfType<T>();

        if (instance != null)
        {
            return instance;
        }

        var name = typeof(T).Name + "Singleton";
        var containerGameObj = new GameObject(name);

        var singletonComponent = containerGameObj.AddComponent<T>();
        
        return singletonComponent;
    }
}