using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public const float LandShake = 0.2f;
    public const float HitShake = 0.5f;
    public const float DeathShake = 3f;
    public MonoBehaviour[] screenScripts;
    public Vector3 shakeVector = Vector3.up;

    private void Update() {}

    public void Shake(float shakeTime) {
        iTween.ShakePosition(gameObject, new Vector3(0.5f, 0.5f, 0f), shakeTime);
    }
}
