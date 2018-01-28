using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour {
    public float rotationSpeed;

    private void Start() {
        GameManager.GetInstance().soundManager.PlaySound("radar_loop_SFX", 1.0f, true);
    }

    private void Update() {
        transform.Rotate(rotationSpeed * Time.deltaTime * Vector3.back);
    }
}
