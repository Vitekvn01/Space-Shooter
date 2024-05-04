using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class LevelCompletionScore : LevelCondition
    {
        [SerializeField] private int _score;

        public override bool IsCompleted
        {
            get
            {
                if (Player.Instance.ActiveShip == null) return false;

                if (Player.Instance.Score >= _score)
                {
                    return true;
                }

                return false;
            }
        }
    }
}

