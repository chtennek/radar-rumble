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
    private ProjectileSpawner spawner;

    private void Awake()
    {
        input = GetComponent<InputReceiver>();
        spawner = GetComponent<ProjectileSpawner>();
    }

    private void FixedUpdate()
    {
        if (input.GetButtonDown("Fire") || (rapidFire && input.GetButton("Fire")))
        {
            if (Time.time - lastFiredTimestamp >= fireCooldown && (projectileLimit < 0 || spawner.projectilesFired.Count < projectileLimit))
            {
                lastFiredTimestamp = Time.time;
                spawner.Fire();
            }
        }
    }
}
