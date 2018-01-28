using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SonarPing : MonoBehaviour {
    public float pingTime = .5f;
    public float targetAlpha = 0f;
    public SpriteRenderer pingSprite;

    private Color startColor;
    private float spawnTimestamp;

    private void Start() {
        startColor = GetComponent<SpriteRenderer>().color;
        spawnTimestamp = Time.time;
    }

    private void Update() {
        pingSprite.color = Color.Lerp(startColor, new Color(1, 1, 1, targetAlpha), (Time.time - spawnTimestamp) / pingTime);
        if (Time.time - spawnTimestamp > pingTime) {
            Destroy(gameObject);
        }
    }

}
