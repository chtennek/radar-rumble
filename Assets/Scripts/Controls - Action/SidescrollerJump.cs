using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(InputReceiver))]
[RequireComponent(typeof(SidescrollerControlManager))]
public class SidescrollerJump : MonoBehaviour
{
    public float jumpDelay = 1f;
    public float jumpSpeed = 8f;
    public float jumpReleaseGravityScale = 12f;
    public float fallGravityScale = 4f;

    public int maxDoubleJumps = 1;
    public bool allowWallJump = true;
    public Vector2 wallJumpVelocity = new Vector2(0, 8);
    public bool wallRefreshesDoubleJumps = true;

    private float lastJumpInput = Mathf.Infinity;
    private int doubleJumpsLeft = 1;

    private InputReceiver input;
    private Rigidbody2D rb;
    private SidescrollerControlManager manager;
    private PingSpawner pingSpawner;

    private void Awake()
    {
        input = GetComponent<InputReceiver>();
        rb = GetComponent<Rigidbody2D>();
        manager = GetComponent<SidescrollerControlManager>();
        pingSpawner = GetComponent<PingSpawner>();
        doubleJumpsLeft = maxDoubleJumps;
    }

    private void FixedUpdate()
    {
        if (manager.IsGrounded() && rb.gravityScale == fallGravityScale)
        {
            pingSpawner.Reveal(PingSpawner.Land);
        }

        // Refresh double jumps
        if (doubleJumpsLeft != maxDoubleJumps)
        {
            if (manager.IsGrounded() || (wallRefreshesDoubleJumps && (manager.IsGrounded(Vector2.left) || manager.IsGrounded(Vector2.right))))
            {
                doubleJumpsLeft = maxDoubleJumps;
            }
        }

        // Jump if we are able to
        if (input.GetButtonDown("Jump")) {
            lastJumpInput = Time.time;
        }

        if (Time.time - lastJumpInput >= jumpDelay)
        {
            lastJumpInput = Mathf.Infinity;
            float targetSpeed = jumpSpeed - rb.velocity.y;

            // Grounded jump
            if (manager.IsGrounded())
            {
                rb.AddForce(targetSpeed * Vector2.up, ForceMode2D.Impulse);
                pingSpawner.Reveal(PingSpawner.Jump);
            }
            // Wall jump
            else if (allowWallJump && (manager.IsGrounded(Vector2.left) || manager.IsGrounded(Vector2.right)))
            {
                Vector2 targetVelocity;
                if (manager.IsGrounded(Vector2.left))
                {
                    targetVelocity = new Vector2(wallJumpVelocity.x - rb.velocity.x, wallJumpVelocity.y - rb.velocity.y);
                }
                else
                {
                    targetVelocity = new Vector2(-wallJumpVelocity.x - rb.velocity.x, wallJumpVelocity.y - rb.velocity.y);
                }
                rb.AddForce(targetVelocity, ForceMode2D.Impulse);
                pingSpawner.Reveal(PingSpawner.Jump);
            }
            // Double jump
            else if (doubleJumpsLeft > 0)
            {
                doubleJumpsLeft--;
                rb.AddForce(targetSpeed * Vector2.up, ForceMode2D.Impulse);
                pingSpawner.Reveal(PingSpawner.Jump);
            }
        }

        rb.gravityScale = manager.defaultGravityScale;
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = fallGravityScale;
        }
        if (input.GetButtonUp("Jump"))
        {
            rb.gravityScale = jumpReleaseGravityScale;
        }
    }
}
