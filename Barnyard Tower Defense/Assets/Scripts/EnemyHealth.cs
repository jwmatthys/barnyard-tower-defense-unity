using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHitPoints = 5;
    private int _currentHitPoints;
    
    void OnEnable()
    {
        _currentHitPoints = maxHitPoints;
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    private void ProcessHit()
    {
        _currentHitPoints--;
        if (_currentHitPoints > 0) return;
        gameObject.SetActive(false);
    }
}
