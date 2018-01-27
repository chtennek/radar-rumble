using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public MonoBehaviour[] screenScripts;
    public Vector3 shakeVector = Vector3.up;
    public float shakeTime = 1f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // For debugging/tweaking
        {
            Shake();
        }
    }

    public void Shake()
    {
        iTween.ShakePosition(gameObject, shakeVector, shakeTime);
    }
}
