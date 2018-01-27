﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingSpawner : MonoBehaviour {
    public Transform pingPrefab;

    public void OnTriggerEnter2D(Collider2D c)
    {
        Instantiate(pingPrefab, transform.position, Quaternion.identity);
    }
}
