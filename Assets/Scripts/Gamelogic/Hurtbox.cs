using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputReceiver))]
public class Hurtbox : MonoBehaviour
{
    public StatusBar hp;
    private int playerId;

    private void Awake() {
        playerId = GetComponent<InputReceiver>().playerId;
    }

    public void OnTriggerEnter2D(Collider2D c)
    {
        ProjectileDriver pd = c.GetComponent<ProjectileDriver>();
        if (pd != null && hp != null && pd.playerId != playerId) {
            hp.currentValue -= pd.damage;
            Destroy(pd.gameObject);
        }
    }
}
