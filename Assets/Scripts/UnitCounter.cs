using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCounter : MonoBehaviour
{
    public GameObject[] controllableUnits;

    private bool dragStarted = false;
    private Vector2 dragStartPos;
    private List<GameObject> selectedUnits = new List<GameObject>();
    
    void Update()
    {
        if (selectedUnits.Count > 0)
        {
            Debug.Log("Selected " + selectedUnits.Count);
        }
        if (!dragStarted && Input.GetMouseButtonDown(0))
        {
            Debug.Log("Drag started!");
            dragStarted = true;
            Vector2 mouseClickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dragStartPos = mouseClickPos;
        }
        if (dragStarted && Input.GetMouseButtonUp(0))
        {
            dragStarted = false;
            Vector2 dragStopPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2[] corners = StandardizeCorners(dragStartPos, dragStopPos);
            SelectUnits(corners);
        }
        if (Input.GetMouseButtonDown(1))
        {
            Vector2 sendPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            SendSelectedUnits(sendPosition);
        }
    }
    #region Select Units
    private Vector2[] StandardizeCorners(Vector2 dragStartPos, Vector2 dragStopPos)
    {
        Vector2 bottomLeft = new Vector2(dragStartPos.x < dragStopPos.x ? dragStartPos.x : dragStopPos.x, dragStartPos.y < dragStopPos.y ? dragStartPos.y : dragStopPos.y);
        Vector2 topRight = new Vector2(dragStartPos.x > dragStopPos.x ? dragStartPos.x : dragStopPos.x, dragStartPos.y > dragStopPos.y ? dragStartPos.y : dragStopPos.y);
        Vector2[] corners = { bottomLeft, topRight };
        return corners;
    }
    private void SelectUnits(Vector2[] corners)
    {
        selectedUnits = GetSelectedUnits(corners);
    }
    private List<GameObject> GetSelectedUnits(Vector2[] corners)
    {
        List<GameObject> selectedUnits = new List<GameObject>();
        foreach (var unit in controllableUnits)
        {
            if(IsUnitInBox(unit, corners[0], corners[1])){
                selectedUnits.Add(unit);
            }
        }
        return selectedUnits;
    }
    private bool IsUnitInBox(GameObject unit, Vector2 bottomLeft, Vector2 topRight)
    {
        Vector2 unitPos = unit.transform.position;
        return unitPos.y < topRight.y && unitPos.y > bottomLeft .y && unitPos.x > bottomLeft.x && unitPos.x < topRight.x;
    }
    #endregion

    #region Send Units
    private void SendSelectedUnits(Vector2 sendPosition)
    {
        foreach (GameObject unit in selectedUnits)
        {
            UnitController controller = unit.GetComponent<UnitController>();
            if (controller)
            {
                GameObject goTo = new GameObject();
                goTo.transform.position = sendPosition;
                if (selectedUnits.Count == 1)
                {
                    controller.GoTowards(goTo, 0.1f);
                }
                else
                {
                    controller.GoTowards(goTo, -1);
                }
            }
        }
    }
    #endregion
}
