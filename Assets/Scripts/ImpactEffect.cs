using UnityEngine;
using Common;

namespace Common
{
    public class ImpactEffect : MonoBehaviour
    {
        [SerializeField] private float _lifetime;

        private float _timer;

        private void Update()
        {
            if (_timer < _lifetime)
                _timer += Time.deltaTime;
            else
                Destroy(gameObject);
        }
    }
}
