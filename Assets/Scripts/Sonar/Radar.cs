using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour {
    public float rotationSpeed;

    private void Update() {
        transform.Rotate(rotationSpeed * Time.deltaTime * Vector3.back);
    }
}
