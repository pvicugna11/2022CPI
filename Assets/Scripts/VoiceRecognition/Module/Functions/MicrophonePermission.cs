using System.Collections;
using System.Collections.Generic;
using UnityEngine;
# if PLATFORM_ANDROID
using UnityEngine.Android;
# endif

public class MicrophonePermission : MonoBehaviour 
{
    GameObject dialog = null;
    
    void Start ()
    {
        #if PLATFORM_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            Permission.RequestUserPermission(Permission.Microphone);
            dialog = new GameObject();
            }
        #endif
    }

}
