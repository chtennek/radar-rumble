using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {
    public  float maxSize = 12f;
    public float waveSpeed = 1f;

    private void Awake() {
        transform.localScale = Vector3.zero;
    }

    private void Update() {
        transform.localScale += Time.deltaTime * waveSpeed * Vector3.one;
        if (transform.localScale.x > maxSize) {
            Destroy(gameObject);
        }
    }
}
