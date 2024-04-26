using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Tower towerPrefab;
    [SerializeField] private bool isPlacable;
    public bool IsPlacable { get { return isPlacable; } }
    private GridManager gridManager;
    private Vector2Int coordinates;
    private Pathfinder pathfinder;
    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }

    private void Start()
    {
        if (gridManager)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
            if (!isPlacable)
            {
                gridManager.BlockNode(coordinates);
            }
        }
    }

    private void OnMouseDown()
    {
        
        if (gridManager.GetNode(coordinates).isWalkable && !pathfinder.WillBlockPath(coordinates))
        {
            bool isSuccessful = towerPrefab.CreateTower(towerPrefab, transform.position);
            if (isSuccessful)
            {
                gridManager.BlockNode(coordinates);
                pathfinder.NotifyReceivers();
            }
        }
    }
}
