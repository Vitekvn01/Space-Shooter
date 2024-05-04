using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] private Transform _target;

        [SerializeField] private float _interpolationLinear;

        [SerializeField] private float _interpolationAngular;

        [SerializeField] private float _cameraZOffset;

        [SerializeField] private float _forwardoffset;

        private void FixedUpdate()
        {
            if (_target == null) return;

            Vector2 camPos = transform.position;
            Vector2 targetPos = _target.position + _target.transform.up * _forwardoffset;

            Vector2 newCamPos = Vector2.Lerp(camPos, targetPos, _interpolationLinear * Time.deltaTime);

            transform.position = new Vector3(newCamPos.x, newCamPos.y, _cameraZOffset);

            if (_interpolationAngular > 0)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, _target.rotation, _interpolationAngular * Time.deltaTime);
            }
        }

        public void SetTarget(Transform newTarget)
        {
            _target = newTarget;
        }
    }
}

