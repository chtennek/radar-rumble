﻿using UnityEngine;
    // Constants
    private const int NUM_PARTICLES = 8;
    // References
    public GameObject particlePrefab;
    // Properties
    private List<Particle> _particles;
    public float fadeOutDuration;

    // ========================= Initialization =========================
    // Use this for initialization
    void Start() {
        // Set references
        myTransform = this.gameObject.transform;

        // Create pool of particles

    // ========================= Methods =========================
    public void SpawnParticles() {
        // Turn on particles
        for (int i=0; i< NUM_PARTICLES; ++i) {
            _particles[i].Activate(myTransform.position,
                new Vector3(Random.Range(0f, 360f), 1f, 0));
        }
    }