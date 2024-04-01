using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushTaskDestroy : MonoBehaviour
{
    private float _timer = 0;

    [SerializeField] private float _timerEnd;
    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > _timerEnd)
        {
            Destroy(gameObject);
        }


    }
}
