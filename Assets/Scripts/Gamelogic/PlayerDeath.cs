using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public StatusBar hp;
    public Vector2 deathVelocity = 2.5f * Vector3.up;
    public float deathAnimationTime = 4f;
    private float timeOfDeath = Mathf.Infinity;

    private SidescrollerControlManager manager;
    private InputReceiver input;
    private PingSpawner pinger;
    private SpriteRenderer playerSprite;

    private void Awake()
    {
        if (hp == null)
        {
            this.enabled = false;
        }
        manager = GetComponent<SidescrollerControlManager>();
        input = GetComponent<InputReceiver>();
        pinger = GetComponent<PingSpawner>();
        playerSprite = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (hp.currentValue <= hp.minValue && input.enabled)
        {
            OnDeath();
        }
        else if (manager.IsGrounded() && !input.enabled && Time.time - timeOfDeath >= .1f)
        {
            playerSprite.sprite = pinger.sprites[PingSpawner.Die];
        }

        if (Time.time - timeOfDeath >= deathAnimationTime)
        {
            Debug.Log("Go to menu-main scene");
        }
    }

    private void OnDeath()
    {
        timeOfDeath = Time.time;
        foreach (MonoBehaviour script in GetComponents<MonoBehaviour>())
        {
            if (script != this)
            {
                script.enabled = false;
            }
        }
        playerSprite.sprite = pinger.sprites[PingSpawner.Hurt];
        GetComponent<Rigidbody2D>().velocity = deathVelocity;
    }
}