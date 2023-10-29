using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionBoxRenderer : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Vector2 dragStartPos;
    private Vector2 dragCurrentPos;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 5;
    }


    void Update()
    {
        UpdateLineVisibility();
        UpdateDragPositions();
        Vector2[] corners = Utils.Vectors.StandardizeCorners(dragStartPos, dragCurrentPos);
        UpdateLines(corners);
    }
    private void UpdateLineVisibility()
    {
        lineRenderer.enabled = Input.GetMouseButton(0);
    }
    private void UpdateDragPositions()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragStartPos = Utils.Mouse.GetMousePosition();
        }
        if (Input.GetMouseButton(0))
        {
            dragCurrentPos = Utils.Mouse.GetMousePosition();
        }
    }
    private void UpdateLines(Vector2[] corners)
    {
        Vector2 topLeft = corners[0];
        Vector2 botRight = corners[1];

        Vector3[] positions = new Vector3[5];
        positions[0] = topLeft;
        positions[1] = new Vector2(botRight.x, topLeft.y);
        positions[2] = botRight;
        positions[3] = new Vector2(topLeft.x, botRight.y);
        positions[4] = topLeft;
        lineRenderer.SetPositions(positions);
    }
}
