using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFollowObject : MonoBehaviour
{
    public Transform[] objects;
    public float timeToDestination = .1f;

    // [TODO] allow objects to deviate from center of screen before moving

    private void Update()
    {
        Vector3 target = CalculateCenterPoint();
        iTween.MoveUpdate(gameObject, target, timeToDestination);
    }

    private Vector3 CalculateCenterPoint()
    {
        if (objects.Length == 0)
        {
            return Vector3.zero;
        }

        Vector3 sum = Vector3.zero;
        foreach (Transform t in objects)
        {
            sum += t.position;
        }

        sum.z = transform.position.z; // Keep the camera at a fixed depth
        return sum / objects.Length;
    }
}
