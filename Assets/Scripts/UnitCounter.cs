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
        if (!dragStarted && Input.GetMouseButtonDown(0))
        {
            dragStarted = true;
            Vector2 mouseClickPos = Utils.Mouse.GetMousePosition();
            dragStartPos = mouseClickPos;
        }
        if (dragStarted && Input.GetMouseButtonUp(0))
        {
            dragStarted = false;
            Vector2 dragStopPos = Utils.Mouse.GetMousePosition();

            Vector2[] corners = Utils.Vectors.StandardizeCorners(dragStartPos, dragStopPos);
            SelectUnits(corners);
        }
        if (Input.GetMouseButtonDown(1))
        {
            Vector2 sendPosition = Utils.Mouse.GetMousePosition();
            SendSelectedUnits(sendPosition);
        }
    }
    #region Select Units
    private void SelectUnits(Vector2[] corners)
    {
        selectedUnits = GetSelectedUnits(corners);
    }
    private List<GameObject> GetSelectedUnits(Vector2[] corners)
    {
        List<GameObject> selectedUnits = new List<GameObject>();
        foreach (var unit in controllableUnits)
        {
            if(Utils.Vectors.IsPositionInBox(unit.transform.position, corners[0], corners[1])){
                selectedUnits.Add(unit);
            }
        }
        return selectedUnits;
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
