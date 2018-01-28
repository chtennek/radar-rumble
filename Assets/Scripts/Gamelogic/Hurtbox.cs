using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputReceiver))]
public class Hurtbox : MonoBehaviour
{
    public StatusBar hp;
    private int playerId;
    private PingSpawner pingSpawner;

    private void Awake()
    {
        playerId = GetComponent<InputReceiver>().playerId;
        pingSpawner = GetComponent<PingSpawner>();
    }

    public void OnTriggerEnter2D(Collider2D c)
    {
        ProjectileDriver pd = c.GetComponent<ProjectileDriver>();
        if (pd != null && hp != null && pd.playerId != playerId)
        {
            // Play some sounds
            if (pd.damage < 50) {
                GameManager.GetInstance().soundManager.PlaySound("hit_rocket", 1f, false);
            }
            else {
                GameManager.GetInstance().soundManager.PlaySound("hit_melee", 1f, false);
            }
            GameManager.GetInstance().soundController.PlayHitChatter();

            // Register damage
            hp.currentValue -= pd.damage;
            if (hp.currentValue > 0)
            {
                pingSpawner.Reveal(PingSpawner.Hurt);
            }
            Destroy(pd.gameObject);
        }
    }
}
