using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarPing : MonoBehaviour {
    public float pingTime = .5f;
    public float targetAlpha = 0f;
    public SpriteRenderer pingSprite;

    private float spawnTimestamp;

    private void Awake() {
        spawnTimestamp = Time.time;
    }

    private void Update() {
        pingSprite.color = Color.Lerp(Color.white, new Color(1, 1, 1, targetAlpha), (Time.time - spawnTimestamp) / pingTime);
        if (Time.time - spawnTimestamp > pingTime) {
            Destroy(gameObject);
        }
    }

}
