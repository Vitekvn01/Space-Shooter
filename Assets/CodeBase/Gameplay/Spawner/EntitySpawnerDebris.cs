using UnityEngine;
using Common;

namespace SpaceShooter
{
    public class EntitySpawnerDebris : MonoBehaviour
    {
        [SerializeField] private Destructible[] _debrisPrefabs;

        [SerializeField] private CircleArea _area;

        [SerializeField] private int _numDebris;

        [SerializeField] private float _randomSpeed;

        private void Start()
        {
            for (int i = 0; i < _numDebris; i++)
            {
                SpawnDebris();
            }
        }

        private void SpawnDebris()
        {
            int index = Random.Range(0, _debrisPrefabs.Length);

            GameObject debris = Instantiate(_debrisPrefabs[index].gameObject);

            debris.transform.position = _area.GetRandomInsideZone();
            debris.GetComponent<Destructible>().EventOnDeath.AddListener(OnDebrisDead);

            Rigidbody2D rb = debris.GetComponent<Rigidbody2D>();

            if (rb != null && _randomSpeed > 0)
            {
                rb.velocity = (Vector2)UnityEngine.Random.insideUnitSphere * _randomSpeed;
            }
        }

        private void OnDebrisDead()
        {
            SpawnDebris();
        }
    }   
  
}
