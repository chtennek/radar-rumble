﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDriver : MonoBehaviour
{
    public float lifetime = Mathf.Infinity;
    public int playerId = -1;

    public int damage = 0;

    [HideInInspector]
    public Transform sourceObject;
    private float spawnTimestamp;

    private void Awake()
    {
        spawnTimestamp = Time.time;
    }

    private void FixedUpdate()
    {
        if (Time.time - spawnTimestamp > lifetime)
        {
            TearDown();
        }
    }

    private void TearDown()
    {
        GameManager.GetInstance().soundManager.PlaySound("despawn_rocket", 1f, false);
        sourceObject.SendMessage("OnProjectileDeletion", transform);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag == "Projectile") {
            TearDown();
        }
    }
}
