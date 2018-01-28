using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputReceiver))]
[RequireComponent(typeof(ProjectileSpawner))]
public class FireProjectileByInput : MonoBehaviour
{
    public string inputName = "Fire";
    public int spriteIndex;
    public float fireCooldown;
    public bool rapidFire; // Continue firing when input is held
    public int projectileLimit = -1; // Set to negative value for no limit

    private float lastFiredTimestamp = -Mathf.Infinity;

    private InputReceiver input;
    public ProjectileSpawner projectileSpawner;
    private PingSpawner pingSpawner;
    private SpriteRenderer playerSprite;

    private void Awake()
    {
        input = GetComponent<InputReceiver>();
        if (projectileSpawner == null)
        {
            projectileSpawner = GetComponent<ProjectileSpawner>();
        }
        pingSpawner = GetComponent<PingSpawner>();
        playerSprite = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (input.GetButtonDown(inputName) || (rapidFire && input.GetButton(inputName)))
        {
            if (Time.time - lastFiredTimestamp >= fireCooldown && (projectileLimit < 0 || projectileSpawner.projectilesFired.Count < projectileLimit))
            {
                lastFiredTimestamp = Time.time;
                pingSpawner.Reveal(spriteIndex);
                if (inputName == "Fire") {
                    GameManager.GetInstance().soundManager.PlaySound("use_rocket_SFX", 1f, false);
                }
                else if (inputName == "Melee") {
                    GameManager.GetInstance().soundManager.PlaySound("use_melee_SFX", 1f, false);
                }
                List<Transform> projectiles = projectileSpawner.Fire();
                foreach (Transform p in projectiles)
                {
                    SpriteRenderer sprite = p.GetComponent<SpriteRenderer>();
                    if (sprite != null)
                    {
                        sprite.color = playerSprite.color;
                    }
                    if (transform.eulerAngles != Vector3.zero)
                    {
                        p.RotateAround(p.position, Vector3.up, 180);
                    }
                }
            }
        }
    }
}
