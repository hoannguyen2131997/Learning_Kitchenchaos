using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonUsingGenerics<T>  : MonoBehaviour where T : Component
{
#if (UNITY_EDITOR_SKIP) 
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));
                if (instance == null)
                {
                    SetupInstance();
                }
            }
            return instance;
        }
    }
    public virtual void Awake()
    {
        RemoveDuplicates();
    }
    private static void SetupInstance()
    {
        instance = (T)FindObjectOfType(typeof(T));
        if (instance == null)
        {
            GameObject gameObj = new GameObject();
            gameObj.name = typeof(T).Name;
            instance = gameObj.AddComponent<T>();
            DontDestroyOnLoad(gameObj);
        }
    }
    private void RemoveDuplicates()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    // Using
    /*
    public class GameManager: SingletonUsingGenerics<GameManager>
    {
        // ...
    }
    */
    // Then you can always refer to the public static GameManager.Instance whenever you need it.
    
#endif
}


