using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(InputReceiver))]
public class TopdownFreeWalk : MonoBehaviour
{
    public float inputDeadZone = .2f;

    [Space]

    public float walkSpeed = 5f;
    public float minWalkableSpeed = 1f;
    public float walkSpeedLevels = Mathf.Infinity;

    [Space]

    public float acceleration = 40; // How fast do we accelerate to walkSpeed?
    public float deceleration = 10; // How fast do we stop when not moving?

    [Space]

    public bool faceMovementDirection;
    public bool onlyMoveForward;
    public float rotationOffset = 90f; // At what movement direction should we be at 0 rotation?
    public float turnSpeed = Mathf.Infinity; // Degrees per frame

    private InputReceiver input;
    private Rigidbody2D rb;
    private Animator anim;

    private void Awake()
    {
        input = GetComponent<InputReceiver>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Vector2 movement = input.GetCircularMovementVector(inputDeadZone);
        bool isFacingMovementDirection = true;
        Vector2 targetVelocity;

        // Change rotation
        if (faceMovementDirection && movement != Vector2.zero)
        {
            float rotationTarget = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg + rotationOffset;
            float rotationDelta = Mathv.ClampAngle180(rotationTarget - transform.eulerAngles.z);
            isFacingMovementDirection = Mathf.Abs(rotationDelta) <= turnSpeed;
            rotationDelta = (rotationDelta > 0) ? Mathf.Min(rotationDelta, turnSpeed) : Mathf.Max(rotationDelta, -turnSpeed);
            transform.Rotate(rotationDelta * Vector3.forward);
        }

        // Change velocity
        float tq = Mathv.LerpQRound(0, 1, Mathf.InverseLerp(inputDeadZone, 1, movement.magnitude), walkSpeedLevels);
        if (onlyMoveForward)
        {
            if (isFacingMovementDirection)
            {
                targetVelocity = Mathf.Lerp(minWalkableSpeed, walkSpeed, tq) * movement.normalized;
            }
            else
            {
                targetVelocity = minWalkableSpeed * transform.TransformDirection(Quaternion.AngleAxis(rotationOffset, Vector3.forward) * Vector3.right);
            }
        }
        else
        {
            targetVelocity = Mathf.Lerp(minWalkableSpeed, walkSpeed, tq) * movement.normalized;
        }
        if (acceleration == Mathf.Infinity)
        {
            rb.velocity = targetVelocity;
        }
        else
        {
            rb.AddForce(acceleration * targetVelocity);
            if (targetVelocity.magnitude == 0)
            {
                rb.drag = deceleration;
            }
            else
            {
                float idealDrag = acceleration / targetVelocity.magnitude;
                rb.drag = acceleration / (acceleration * Time.deltaTime + 1); // [TODO] work around PhysX drag approximation
            }
        }

        UpdateAnimation();
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
