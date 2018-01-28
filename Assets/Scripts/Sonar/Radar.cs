using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour {
    public float rotationSpeed;

    private void Start() {
        GameManager.GetInstance().soundManager.PlayUniqueSound("radar_loop_SFX", 0.15f, true);
        transform.eulerAngles = GameManager.GetInstance().dataManager.radarRotation;
    }

    private void Update() {
        transform.Rotate(rotationSpeed * Time.deltaTime * Vector3.back);
    }
}
