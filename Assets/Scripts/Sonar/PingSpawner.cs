using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PingSpawner : MonoBehaviour {
    public const int MeleeAttack = 0;
    public const int ShootAttack = 1;
    public const int Hurt = 2;
    public const int Die = 3;

    public Transform pingPrefab;
    public Sprite[] sprites;
    private PlayerProperties properties;
    private SpriteRenderer playerSprite;

    private void Start() {
        playerSprite = GetComponent<SpriteRenderer>();
        properties = GetComponent<PlayerProperties>();
    }

    public void OnTriggerEnter2D(Collider2D c)
    {
        SpriteRenderer spriteRenderer = CreatePingRenderer();
        if (properties.isShooting) {
            properties.isShooting = false;
            spriteRenderer.sprite = sprites[5];
        }
        else if (properties.isWalking) {
            spriteRenderer.sprite = (Random.Range(0f, 1f) < 0.5f) ? sprites[0] : sprites[1];
        }
        else if (properties.isGrounded) {
            spriteRenderer.sprite = sprites[0];
        }
        else if (!properties.isGrounded) {
            spriteRenderer.sprite = sprites[4];
        }
    }

    public void Reveal(int frame) {
        SpriteRenderer spriteRenderer = CreatePingRenderer();
        switch (frame) {
            case PingSpawner.MeleeAttack:
                spriteRenderer.sprite = sprites[3];
                break;
            case PingSpawner.ShootAttack:
                spriteRenderer.sprite = sprites[5];
                break;
            case PingSpawner.Hurt:
                spriteRenderer.sprite = sprites[6];
                break;
            case PingSpawner.Die:
                spriteRenderer.sprite = sprites[7];
                break;
        }
    }

    public SpriteRenderer CreatePingRenderer() {
        Transform ping = Instantiate(pingPrefab, transform.position, Quaternion.identity);
        SpriteRenderer spriteRenderer = ping.GetComponent<SpriteRenderer>();
        if (!properties.isFacingRight) {
            ping.localScale = new Vector3(-1f, 1f, 1f);
        }
        spriteRenderer.color = playerSprite.color;
        return spriteRenderer;
    }
}
