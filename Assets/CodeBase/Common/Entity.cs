using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public abstract class Entity : MonoBehaviour
    {
        [SerializeField] private string _nickname;
        public string Nickname => _nickname;
    }
}

