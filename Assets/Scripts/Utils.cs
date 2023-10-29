using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static class Mouse
    {
        public static Vector2 GetMousePosition()
        {
            return (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
    public static class Vectors
    {
        public static Vector2[] StandardizeCorners(Vector2 dragStartPos, Vector2 dragStopPos)
        {
            Vector2 bottomLeft = new Vector2(dragStartPos.x < dragStopPos.x ? dragStartPos.x : dragStopPos.x, dragStartPos.y < dragStopPos.y ? dragStartPos.y : dragStopPos.y);
            Vector2 topRight = new Vector2(dragStartPos.x > dragStopPos.x ? dragStartPos.x : dragStopPos.x, dragStartPos.y > dragStopPos.y ? dragStartPos.y : dragStopPos.y);
            Vector2[] corners = { bottomLeft, topRight };
            return corners;
        }
        public static bool IsPositionInBox(Vector2 pos, Vector2 bottomLeft, Vector2 topRight)
        {
            Vector2 unitPos = pos;
            return unitPos.y < topRight.y && unitPos.y > bottomLeft.y && unitPos.x > bottomLeft.x && unitPos.x < topRight.x;
        }
    }
}
