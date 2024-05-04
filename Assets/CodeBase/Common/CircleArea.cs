using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif
namespace Common
{
    public class CircleArea : MonoBehaviour
    {
        [SerializeField] private float _radius;
        public float Radius => _radius;

        public Vector2 GetRandomInsideZone()
        {
            return (Vector2)transform.position + (Vector2)UnityEngine.Random.insideUnitSphere * _radius;
        }

        private static Color GizmoColor = new Color(0, 1, 0, 0.3f);

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Handles.color = GizmoColor;
            Handles.DrawSolidDisc(transform.position, transform.forward, _radius);
        }

#endif      
    }
}
