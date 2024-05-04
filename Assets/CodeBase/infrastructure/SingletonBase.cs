using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public abstract class SingletonBase<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    public void Init()
    {
        if (Instance != null)
        {
            Debug.LogWarning("MonoSingleton: object of type already extist, instance will be destroyed = " + typeof(T).Name);
            Destroy(this);
            return;
        }

        Instance = this as T;
    }
}
