using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    public int playerId;
    public StatusBar hp;

    public void OnTriggerEnter2D(Collider2D c)
    {
        ProjectileDriver pd = c.GetComponent<ProjectileDriver>();
        if (pd != null && hp != null && pd.playerId != playerId) {
            hp.currentValue -= pd.damage;
            Destroy(pd.gameObject);
        }
    }
}
