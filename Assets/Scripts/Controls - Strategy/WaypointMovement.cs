using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class WaypointMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Queue<Vector2> waypoints = new Queue<Vector2>();

    private Vector2 waypointLastUpdate = Mathf.Infinity * Vector2.one;

    private Rigidbody2D rb;
    private Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (waypoints.Count > 0)
        {
            Debug.Log(rb.velocity);
        }
        Vector2 waypoint = GetNextWaypoint();
        Vector2 direction = (waypoint - rb.position).normalized;

        rb.velocity = moveSpeed * direction;
        UpdateAnimation();
    }

    private Vector2 GetNextWaypoint()
    {
        while (waypoints.Count > 0)
        {
            Vector2 waypoint = waypoints.Peek();

            // We can't get to the waypoint, so give up
            if (waypoint == waypointLastUpdate && rb.velocity == Vector2.zero)
            {
                waypoints.Dequeue();
                continue;
            }

            // Return the waypoint if we are far enough away from it
            if (Vector2.Distance(rb.position, waypoint) >= Time.deltaTime * moveSpeed)
            {
                waypointLastUpdate = waypoint;
                return waypoint;
            }
            waypoints.Dequeue();
        }
        return rb.position;
    }

    private void UpdateAnimation()
    {
        if (anim != null)
        {
            anim.SetFloat("xVelocity", rb.velocity.x);
            anim.SetFloat("yVelocity", rb.velocity.y);
        }
    }
}
