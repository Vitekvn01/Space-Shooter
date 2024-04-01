using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Transform _camera;

        [SerializeField] private float _interpolationLinear;

        [SerializeField] private float _interpolationAngular;

        [SerializeField] private float _cameraZOffset;

        [SerializeField] private float _forwardoffset;

        private void FixedUpdate()
        {
            if (_target == null) return;

            Vector2 camPos = _camera.transform.position;
            Vector2 targetPos = _target.position + _target.transform.up * _forwardoffset;

            Vector2 newCamPos = Vector2.Lerp(camPos, targetPos, _interpolationLinear * Time.deltaTime);

            _camera.position = new Vector3(newCamPos.x, newCamPos.y, _cameraZOffset);

            if (_interpolationAngular > 0)
            {
                _camera.transform.rotation = Quaternion.Slerp(_camera.transform.rotation,
                                                               _target.rotation, _interpolationAngular * Time.deltaTime);
            }
        }

        public void SetTarget(Transform newTarget)
        {
            _target = newTarget;
        }
    }
}

