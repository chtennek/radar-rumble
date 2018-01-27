using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class InvertedCircleCollider2D : MonoBehaviour {
    public int edgeCount;
    public float radius;

    private EdgeCollider2D edgeCollider;

    private void Awake() {
        edgeCollider = GetComponent<EdgeCollider2D>();
    }

    private void Update() {
        if (edgeCollider.points.Length == 0 || radius != edgeCollider.points[0].x) {
            MakeCollider();
        }
    }

    private void MakeCollider()
    {
        Vector2[] points = new Vector2[edgeCount];

        for (int i = 0; i < edgeCount; i++)
        {
            float angle = 2 * Mathf.PI * i / edgeCount;
            float x = radius * Mathf.Cos(angle);
            float y = radius * Mathf.Sin(angle);

            points[i] = new Vector2(x, y);
        }
        edgeCollider.points = points;
    }
}
