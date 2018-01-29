using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(InputReceiver))]
public class SidescrollerPlatformDrop : MonoBehaviour {
    public float inputDeadZone = .2f;

    public int defaultLayer = 8;
    public int dropThroughLayer = 13;

    public int playerLayer = 9;
    public int playerDropThroughLayer = 12;

    private bool isDropping = false;
    private float dropDuration = 2f;
    private float lastDropTime = -Mathf.Infinity;
    private SidescrollerControlManager manager;

    private InputReceiver input;
    private Rigidbody2D rb;
    private Collider2D col;

    private void Awake()
    {
        manager = GetComponent<SidescrollerControlManager>();
        input = GetComponent<InputReceiver>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        isDropping = manager.IsGrounded();
    }

    private void Update()
    {
        float movement = input.GetQuantizedMovementVector().y;
        if (movement < 0 && Time.time - lastDropTime > dropDuration && manager.IsGrounded())
        {
            lastDropTime = Time.time;
        }

        if (!isDropping && Time.time - lastDropTime < dropDuration)
        {
            isDropping = true;
            gameObject.layer = playerDropThroughLayer;
            manager.groundMask = (1 << defaultLayer);
        }
        else if (isDropping && Time.time - lastDropTime >= dropDuration) {
            isDropping = false;
            gameObject.layer = playerLayer;
            manager.groundMask = (1 << defaultLayer) | (1 << dropThroughLayer);
        }
    }
}
