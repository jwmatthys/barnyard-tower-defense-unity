using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class AutosnapToGrid : MonoBehaviour
{
    [Tooltip("Grid Size should match Unity grid size setting.")]
    [SerializeField] private int unityGridSize = 10;
    void Update()
    {
        if (Application.isPlaying) return;
        Vector3 newPosition = transform.position;
        newPosition.x = Mathf.Round(transform.position.x / unityGridSize) * unityGridSize;
        newPosition.y = 0;
        newPosition.z = Mathf.Round(transform.position.z / unityGridSize) * unityGridSize;
        transform.position = newPosition;
    }
}
