using UnityEditor;
using UnityEngine;

namespace SpaceShooter
{
    public class LevelCompletionPosition : LevelCondition
    {
        [SerializeField] private float _radius;

        public override bool IsCompleted
        {
            get
            {
                if (Player.Instance.ActiveShip == null) return false;

                if (Vector3.Distance(Player.Instance.ActiveShip.transform.position, transform.position) <= _radius)
                {
                    return true;
                }

                return false;
            }
        }

#if UNITY_EDITOR

        private static Color GizmoColor = new Color(0, 1, 0, 0.3f);

        private void OnDrawGizmosSelected()
        {
            Handles.color = GizmoColor;
            Handles.DrawSolidDisc(transform.position, transform.forward, _radius);
        }

#endif

    }
}

