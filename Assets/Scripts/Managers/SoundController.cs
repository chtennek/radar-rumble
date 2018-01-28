using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {
    private SoundManager soundManager;
    private string[] onHitChatterTypes;
    private string[] randomChatterTypes;
    private float chatterCooldownTimer = 10f;

    // Use this for initialization
    public void Initialize () {
        soundManager = GetComponent<SoundManager>();
        onHitChatterTypes = new string[] { "chatter_aliveOrDead", "chatter_bullseye", "chatter_bullseye2",
            "chatter_getHit1", "chatter_getHit2", "chatter_getHit3", "chatter_hitOnDeadMan" };
        randomChatterTypes = new string[] { "chatter_random_1", "chatter_random_2","chatter_random_3","chatter_random_4",
            "chatter_random_5","chatter_random_6","chatter_random_7","chatter_random_8","chatter_random_9" };
    }

    // Update is called once per frame
    public void Update() {
        chatterCooldownTimer -= Time.deltaTime;
        PlayRandomChatter();
    }

    public void PlayHitChatter () {
        if (chatterCooldownTimer < 0 && Random.Range(0f, 1f) < 0.75f) {
            chatterCooldownTimer = 5f;
            int randomIndex = Random.Range(0, onHitChatterTypes.Length);
            soundManager.PlaySound(onHitChatterTypes[randomIndex], 1f, false);
        }
    }

    public void PlayRandomChatter() {
        if (chatterCooldownTimer < 0 && Random.Range(0f, 1f) < 0.05f) {
            chatterCooldownTimer = 5f;
            int randomIndex = Random.Range(0, randomChatterTypes.Length);
            soundManager.PlaySound(randomChatterTypes[randomIndex], 1f, false);
        }
    }
}