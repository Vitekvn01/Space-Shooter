using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Common
{
    public class SyncTransform : MonoBehaviour
    {
        private Transform _target;

        // Update is called once per frame
        void FixedUpdate()
        {
            transform.position = new Vector3(_target.position.x, _target.position.y, transform.position.z);
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }
    }
}

