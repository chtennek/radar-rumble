using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SidescrollerControlManager : MonoBehaviour
{
    public float defaultGravityScale = 2f;

    public float playerHeight = 0.22f;
    public float playerWidth = 0.22f;
    public float collisionCheckWidth = 0.05f;
    public LayerMask groundMask;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        if (sprite != null)
        {
            if (rb.velocity.x > 0)
            {
                transform.rotation = Quaternion.identity;
            }
            if (rb.velocity.x < 0)
            {
                transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
            }
        }
        if (anim != null)
        {
            anim.SetFloat("xVelocity", rb.velocity.x);
            anim.SetFloat("yVelocity", rb.velocity.y);
            anim.SetFloat("gravityScale", rb.gravityScale);
            anim.SetBool("isGrounded", IsGrounded());
            anim.SetBool("isAgainstWall", IsGrounded(Vector2.left) || IsGrounded(Vector2.right));
        }
    }

    public bool IsGrounded() { return IsGrounded(Vector2.down, groundMask); }
    public bool IsGrounded(Vector2 direction) { return IsGrounded(direction, groundMask); }
    public bool IsGrounded(Vector2 direction, LayerMask colliderMask)
    {
        Vector2 boxCenter = (Vector2)transform.position;
        Vector2 boxSize;
        if (Mathf.Abs(direction.y) > Mathf.Abs(direction.x))
        {
            boxCenter += 0.5f * playerHeight * transform.lossyScale.y * direction;
            boxSize = new Vector2(playerWidth * transform.lossyScale.x - collisionCheckWidth, collisionCheckWidth);
        }
        else
        {
            boxCenter += 0.5f * playerWidth * transform.lossyScale.x * direction;
            boxSize = new Vector2(collisionCheckWidth, playerHeight * transform.lossyScale.y - collisionCheckWidth);
        }
        bool isGrounded = Physics2D.OverlapBox(boxCenter, boxSize, 0, colliderMask);
        return isGrounded;
    }
}
