using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public Transform projectilePrefab;

    public Vector2 spawnOffset; // Relative to the player's location
    public float angleWhenUpright; // Set angle at which the projectile should be upright
    public bool fixRotation; // Use the same rotation for all sub-projectiles, instead of factoring in velocity direction
    public Vector2[] projectileVelocities = new Vector2[1]; // Spawn one projectile for each velocity and for each pellet

    public Vector2 spread; // Randomly modify velocity within spread range
    public int pelletCount = 1; // Use with velocity spread for shotgun effect

    [HideInInspector]
    public List<Transform> projectilesFired = new List<Transform>();

    private InputReceiver input;

    private void Awake() {
        input = GetComponent<InputReceiver>();
    }

    public List<Transform> Fire() { return Fire(Vector2.right); }
    public List<Transform> Fire(Vector2 modifier)
    {
        List<Transform> projectiles = new List<Transform>();
        for (int i = 0; i < pelletCount; i++)
        {
            foreach (Vector2 velocity in projectileVelocities)
            {
                Vector2 modifiedVelocity = Quaternion.AngleAxis(modifier.y, Vector3.forward) * (modifier.x * velocity);
                Transform projectile = SpawnProjectile(modifiedVelocity + new Vector2(Random.Range(-spread.x, spread.x), Random.Range(-spread.y, spread.y)));
                projectiles.Add(projectile);
            }
        }
        return projectiles;
    }

    private Transform SpawnProjectile(Vector2 velocity)
    {
        Vector3 spawnLocation = transform.position + transform.rotation * (Vector3)spawnOffset;
        float rotationFromProjectileVelocity = fixRotation ? 0 : velocity.y;
        Transform projectile = Instantiate(projectilePrefab, spawnLocation, Quaternion.Euler(0, 0, rotationFromProjectileVelocity - angleWhenUpright));
        projectilesFired.Add(projectile);

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        ProjectileDriver pd = projectile.GetComponent<ProjectileDriver>();
        if (rb != null)
        {
            rb.velocity = transform.rotation * (Vector3)Mathv.Pol2Car(velocity);
        }
        if (pd != null)
        {
            pd.sourceObject = transform;
            if (input != null) {
                pd.playerId = input.playerId;
            }
        }
        return projectile;
    }

    public void OnProjectileDeletion(Transform projectile)
    {
        projectilesFired.Remove(projectile);
    }
}
