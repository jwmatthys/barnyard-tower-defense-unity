using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteInEditMode]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private Color blockedColor = Color.gray;
    
    private TextMeshPro _label;
    private Vector2Int _coordinates = new Vector2Int();
    private Waypoint _waypoint;
    
    private void Awake()
    {
        _waypoint = GetComponentInParent<Waypoint>();
        _label = GetComponent<TextMeshPro>();
        _label.enabled = false;
        DisplayCoordinates();
    }

    private void Update()
    {
        DisplayCoordinates();
        UpdateObjectName();
        ColorCoordinates();
        ToggleLabels();
    }

    private void ColorCoordinates()
    {
        _label.color = _waypoint.IsPlacable ? defaultColor : blockedColor;
    }

    private void DisplayCoordinates()
    {
        _coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        _coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);
        _label.text = _coordinates.x + "," + _coordinates.y;
    }

    void UpdateObjectName()
    {
        transform.parent.name = _coordinates.ToString();
    }

    void ToggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            _label.enabled = !_label.IsActive();
        }
    }
}
