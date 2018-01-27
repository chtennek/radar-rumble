using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionType
{
    Move,
    Fire,
    Suicide
}

[System.Serializable]
public struct Action
{
    public ActionType type;
    public Vector2 target;
    public float waitTime; // After current action is executed
}

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(ProjectileSpawner))]
public class TopdownAI : MonoBehaviour
{
    public Action[] actions;
    public float movementSpeed = 1f;
    public bool loopActions = true;

    private int currentCommandIndex;
    private float nextActionTime;

    private Rigidbody2D rb;
    private ProjectileSpawner spawner;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spawner = GetComponent<ProjectileSpawner>();
        nextActionTime = Time.time;
    }

    private void FixedUpdate()
    {
        // Handle action execution
        if (Time.time >= nextActionTime && currentCommandIndex < actions.Length)
        {
            ExecuteAction(actions[currentCommandIndex]);
            nextActionTime = Time.time + actions[currentCommandIndex].waitTime;
            currentCommandIndex++;
            if (loopActions)
            {
                currentCommandIndex %= actions.Length;
            }
        }
    }

    private void ExecuteAction(Action action)
    {
        switch (action.type)
        {
            case ActionType.Move:
                rb.velocity = action.target;
                break;
            case ActionType.Fire:
                spawner.Fire();
                break;
            default:
                Destroy(gameObject);
                break;
        }
    }
}
