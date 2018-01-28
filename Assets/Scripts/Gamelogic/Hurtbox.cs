using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputReceiver))]
public class Hurtbox : MonoBehaviour
{
    public StatusBar hp;
    private int playerId;
    private PingSpawner pingSpawner;

    private void Awake() {
        playerId = GetComponent<InputReceiver>().playerId;
        pingSpawner = GetComponent<PingSpawner>();
    }

    public void OnTriggerEnter2D(Collider2D c)
    {
        ProjectileDriver pd = c.GetComponent<ProjectileDriver>();
        if (pd != null && hp != null && pd.playerId != playerId) {
            hp.currentValue -= pd.damage;
            pingSpawner.Reveal(PingSpawner.Hurt);
            Destroy(pd.gameObject);
        }
    }
}
