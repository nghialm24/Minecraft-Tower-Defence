using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T instance;

    private static bool instanceExisting = false;

    public bool dontDestroyOnLoad = true;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();

                if (instance == null)
                {
                    var singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<T>();
                    instance.name = typeof(T).Name;

                    Debug.LogWarning("Create new instance of " + typeof(T).Name);
                }

                var self = instance as SingletonMonoBehaviour<T>;
                if (self.dontDestroyOnLoad)
                    DontDestroyOnLoad(self.gameObject);
            }

            return instance;
        }
    }

    public static bool IsInstanceExisting
    {
        get { return instanceExisting; }
    }

    public virtual void Awake()
    {
        instanceExisting = true;

        if (instance == null)
        {
            instance = this as T;

            if (dontDestroyOnLoad) 
                DontDestroyOnLoad(instance.gameObject);
        }
        else if (instance.GetInstanceID() != GetInstanceID())
        {
            Destroy(gameObject);
        }
    }

    public virtual void OnDestroy()
    {
        instanceExisting = false;
    }
}
