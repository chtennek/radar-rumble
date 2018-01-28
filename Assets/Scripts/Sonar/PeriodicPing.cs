using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PingSpawner))]
public class PeriodicPing : MonoBehaviour {
    public float timeBetweenPings = .5f;

    private float lastPingTime = -Mathf.Infinity;

    private PingSpawner spawner;

    private void Awake() {
        spawner = GetComponent<PingSpawner>();
    }

    private void Update()
    {
        if (Time.time - lastPingTime >= timeBetweenPings)
        {
            lastPingTime = Time.time;
            spawner.Reveal(PingSpawner.Jump); // Also handles rocket pings (sprite index OOB)
        }
    }
}
