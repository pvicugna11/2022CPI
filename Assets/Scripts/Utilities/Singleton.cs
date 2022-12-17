using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * <summary>
 * シングルトンの親クラス
 * </summary>
 */
public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance)
        {
            Destroy(Instance);
            return;
        }

        Instance = (T)this;
        DontDestroyOnLoad(gameObject);
    }
}
