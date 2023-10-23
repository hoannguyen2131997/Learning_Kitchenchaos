using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonBasic : MonoBehaviour
{
#if UNITY_EDITOR_SKIP
     // Create Access Global (public static) to easy use it
    public static SingletonBasic Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            // check if it’s already set. If Instance is currently null, then Instance gets set to this specific object. This must be the first singleton in the scene
            Instance = this;
        }
        else
        {
            // call Destroy(gameObject) to guarantee your singleton only has one such component in the scene
            Destroy(gameObject);
        }
    }
    //  Note: 
    // ##############################################################################################################################################################################################
    //  it does suffer from two issues:
    //  Loading a new scene destroys the GameObject
    //  You need to set up the singleton in the hierarchy before using it
    
    // you can make a manager object (e.g., game flow manager or audio manager) that is always accessible from every other GameObject in your scene.
    // Also, if you’ve implemented the object pool, you can design your pooling system as a singleton to make getting pooled objects easier
    
    // If you decide to use singletons in your project, keep them to a minimum. Don’t use them indiscriminately. Reserve the singletons for a handful of scripts that can benefit from global access.
    // ##############################################################################################################################################################################################
#endif
   
}
