using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarPing : MonoBehaviour {
    public float pingTime = .5f;

    private float lastPingTimestamp = -Mathf.Infinity;

    private SpriteRenderer sprite;

    private void Awake() {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        sprite.color = Color.Lerp(Color.white, Color.clear, (Time.time - lastPingTimestamp) / pingTime);
    }

    public void OnTriggerEnter2D(Collider2D c) {
        lastPingTimestamp = Time.time;
    }
}
