using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputReceiver))]
[RequireComponent(typeof(ProjectileSpawner))]
public class FireProjectileByInput : MonoBehaviour
{
    public float fireCooldown;
    public bool rapidFire; // Continue firing when input is held
    public int projectileLimit = -1; // Set to negative value for no limit

    private float lastFiredTimestamp = -Mathf.Infinity;

    private InputReceiver input;
    private ProjectileSpawner projectileSpawner;
    private PingSpawner pingSpawner;
    private PlayerProperties properties;
    private SpriteRenderer playerSprite;

    private void Awake()
    {
        input = GetComponent<InputReceiver>();
        projectileSpawner = GetComponent<ProjectileSpawner>();
        pingSpawner = GetComponent<PingSpawner>();
        properties = GetComponent<PlayerProperties>();
        playerSprite = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (input.GetButtonDown("Fire") || (rapidFire && input.GetButton("Fire")))
        {
            if (Time.time - lastFiredTimestamp >= fireCooldown && (projectileLimit < 0 || projectileSpawner.projectilesFired.Count < projectileLimit))
            {
                lastFiredTimestamp = Time.time;
                properties.isShooting = true;
                List<Transform> projectiles = projectileSpawner.Fire();
                foreach (Transform p in projectiles) {
                    SpriteRenderer sprite = p.GetComponent<SpriteRenderer>();
                    if (sprite != null) {
                        sprite.color = playerSprite.color;
                    }
                }
            }
        }
    }
}
