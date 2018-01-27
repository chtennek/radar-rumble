using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD
public class PingSpawner : MonoBehaviour
{
=======
>>>>>>> 5e2fcc24cfe23513be713a4bf118ce7827b9b9f8
    public Transform pingPrefab;

    public void OnTriggerEnter2D(Collider2D c)
    {
<<<<<<< HEAD
        if (c.tag == "Radar")
        {
            Instantiate(pingPrefab, transform.position, Quaternion.identity);
        }
=======
>>>>>>> 5e2fcc24cfe23513be713a4bf118ce7827b9b9f8
    }
}
