using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] private Transform weapon;
    [SerializeField] private float range = 20f;
    private Transform _target;
    private ParticleSystem _projectileParticles;
    private ParticleSystem.EmissionModule _emissionModule;

    private void Awake()
    {
        _projectileParticles = GetComponentInChildren<ParticleSystem>();
        _emissionModule = _projectileParticles.emission;
    }

    private void Start()
    {
        _emissionModule.enabled = false;
    }

    void Update()
    {
        FindClosestTarget();
        if (_target) AimWeapon();
    }

    private void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float maxDistance = Mathf.Infinity;
        foreach (Enemy enemy in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);
            if (targetDistance < maxDistance)
            {
                closestTarget = enemy.transform;
                maxDistance = targetDistance;
            }
        }

        _target = closestTarget;
    }

    private void AimWeapon()
    {
        float targetDistance = Vector3.Distance(transform.position, _target.position);
        weapon.LookAt(_target);
        weapon.Rotate(0,90,0);
        Attack(targetDistance <= range);
    }

    void Attack(bool isActive)
    {
        _emissionModule.enabled = isActive;
    }
}