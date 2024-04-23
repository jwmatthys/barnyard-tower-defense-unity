using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private bool isPlacable;
    public bool IsPlacable { get { return isPlacable; } }
    
    [SerializeField] private GameObject towerPrefab;
    private void OnMouseDown()
    {
        if (!isPlacable) return;
        Instantiate(towerPrefab, transform.position, Quaternion.identity);
        isPlacable = false;
    }
}
