using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EntitySelector))]
public class OrderGridMove : MonoBehaviour
{
    private EntitySelector es;

    private void Awake()
    {
        es = GetComponent<EntitySelector>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector2 destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            foreach (EntityManager entity in es.GetSelectedObjects())
            {
                entity.QueueMoveOrder(destination);
            }
        }
    }
}
