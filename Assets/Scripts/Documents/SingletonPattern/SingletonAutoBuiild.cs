using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonAutoBuiild : MonoBehaviour
{
#if (UNITY_EDITOR_SKIP) 
    // Instance is now a public property referring to the private instance backing field.
    private static SingletonAutoBuiild instance;
    public static SingletonAutoBuiild Instance
    {
        get
        {
            if (instance == null)
            {
                SetupInstance();
            }
            return instance;
        }
    }
    private void Awake()
    {
        // The first time you refer to the singleton, check for the existence of Instance in the getting. If it doesn’t exist, the SetupInstance method creates a GameObject with the appropriate component.
        if (instance == null)
        {
            instance = this;
            // DontDestroyOnLoad(gameObject) prevents a scene load from clearing the singleton from the hierarchy. The singleton instance is now persistent, staying active even if you change scenes in your game.
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private static void SetupInstance()
    {
        instance = FindObjectOfType<SingletonAutoBuiild>();
        if (instance == null)
        {
            // create a GameObject
            GameObject gameObj = new GameObject();
            gameObj.name = "Singleton";
            
            // add the appropriate Singleton component.
            instance = gameObj.AddComponent<SingletonAutoBuiild>();
            DontDestroyOnLoad(gameObj);
        }
    }
#endif
}
