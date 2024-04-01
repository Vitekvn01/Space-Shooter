using SpaceShooter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class LevelBoundaryLimmiter : MonoBehaviour
    {
        private SpaceShip ship;
        /// <summary>
        /// ������������ �������. �������� � ������ c� ��������  LevelBoudary ���� ������� ������� �� �����.
        /// �������� �� ������ ������� ���� ����������.
        /// </summary>
        private void Start()
        {
            ship = GetComponent<SpaceShip>();
        }

        private void Update()
        {
            if (LevelBoundary.Instance == null) return;

            var lb = LevelBoundary.Instance;
            var r = lb.Radius;

            if (transform.position.magnitude > r)
            {
                if (lb.LimitMode == LevelBoundary.Mode.Limit)
                {
                    transform.position = transform.position.normalized * r;
                }

                if (lb.LimitMode == LevelBoundary.Mode.Teleport)
                {
                    transform.position = -transform.position.normalized * r;
                }

                if (lb.LimitMode == LevelBoundary.Mode.Death)
                {
                    ship.ApplyDamge(10000);
                }
            }
        }
    }
}

