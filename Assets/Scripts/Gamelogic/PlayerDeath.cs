using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public StatusBar hp;

    private void Update() {
        if (hp.currentValue <= hp.minValue) {
            OnDeath();
        }
    }

    private void OnDeath() {
        
    }
}