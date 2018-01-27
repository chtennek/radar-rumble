using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    private WaypointMovement wm;
    private Canvas canvas;

    private void Awake()
    {
        wm = GetComponent<WaypointMovement>();
        canvas = GetComponentInChildren<Canvas>();
        if (canvas != null)
        {
            canvas.enabled = false;
        }
    }

    public void OnSelected()
    {
        if (canvas != null)
        {
            canvas.enabled = true;
        }
    }

    public void OnDeselected()
    {
        if (canvas != null)
        {
            canvas.enabled = false;
        }
    }

    public void QueueMoveOrder(Vector2 position)
    {
        if (wm != null)
        {
            wm.waypoints.Enqueue(position);
        }
    }
}
