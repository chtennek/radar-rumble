using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PingSpawner : MonoBehaviour
{
    public const int Idle = 1;
    public const int Walk = 0;
    public const int Jump = 4;
    public const int Land = 2;
    public const int MeleeAttack = 3;
    public const int ShootAttack = 5;
    public const int Hurt = 6;
    public const int Die = 7;

    public Transform pingPrefab;
    public Sprite[] sprites;
    private PlayerProperties properties;
    private SpriteRenderer playerSprite;

    private void Start()
    {
        playerSprite = GetComponent<SpriteRenderer>();
        properties = GetComponent<PlayerProperties>();
    }

    public void OnTriggerEnter2D(Collider2D c)
    {
        if (this.enabled == true && c.tag == "Radar")
        {
            SpriteRenderer spriteRenderer = CreatePingRenderer();
            if (properties.isWalking)
            {
                spriteRenderer.sprite = (Random.Range(0f, 1f) < 0.5f) ? sprites[0] : sprites[1];
            }
            else if (properties.isGrounded)
            {
                spriteRenderer.sprite = sprites[0];
            }
            else if (!properties.isGrounded)
            {
                spriteRenderer.sprite = sprites[4];
            }
        }
    }

    public void Reveal(int frame)
    {
        SpriteRenderer spriteRenderer = CreatePingRenderer();
        spriteRenderer.sprite = sprites[frame];
    }

    public SpriteRenderer CreatePingRenderer()
    {
        Transform ping = Instantiate(pingPrefab, transform.position, Quaternion.identity);
        SpriteRenderer spriteRenderer = ping.GetComponent<SpriteRenderer>();
        if (!properties.isFacingRight)
        {
            ping.localScale = new Vector3(-1f, 1f, 1f);
        }
        spriteRenderer.color = playerSprite.color;
        return spriteRenderer;
    }
}
