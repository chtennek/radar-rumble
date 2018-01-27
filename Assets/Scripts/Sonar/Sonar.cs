using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonar : MonoBehaviour
{
    public Transform wavePrefab;
    public float waveCooldown = 1f;

    private List<Transform> waves = new List<Transform>();
    private float lastWaveSpawn = -Mathf.Infinity;

    private void Update()
    {
        if (Time.time - lastWaveSpawn >= waveCooldown)
        {
            lastWaveSpawn = Time.time;
            Instantiate(wavePrefab, transform.position, Quaternion.identity);
        }
    }
}