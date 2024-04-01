using SpaceShooter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishZone : MonoBehaviour
{
    [SerializeField] private GameObject _finishPanel;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _finishPanel.SetActive(true);
    }

}
