using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] [Range(0f, 5f)] private float speed = 1f;
    private List<Node> path = new List<Node>();
    private Enemy enemy;
    private Pathfinder pathfinder;
    private GridManager gridManager;

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
        enemy = GetComponent<Enemy>();
    }

    void OnEnable()
    {
        ReturnToStart();
        RecalculatePath(true);
    }

    void RecalculatePath(bool resetPath)
    {
        Vector2Int coordinates = new Vector2Int();
        if (resetPath)
        {
            coordinates = pathfinder.StartCoordinates;
        }
        else coordinates = gridManager.GetCoordinatesFromPosition(transform.position);

        StopAllCoroutines();
        path.Clear();
        path = pathfinder.GetNewPath(coordinates);
        StartCoroutine(FollowPath());
    }

    void ReturnToStart()
    {
        transform.position = gridManager.GetPositionFromCoordinates(pathfinder.StartCoordinates);
    }

    IEnumerator FollowPath()
    {
        for (int i = 1; i < path.Count; i++)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = gridManager.GetPositionFromCoordinates(path[i].coordinates);
            float travelPercent = 0f;
            transform.LookAt(endPosition);
            while (travelPercent < 1)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }

        enemy.StealGold();
        gameObject.SetActive(false);
    }
}