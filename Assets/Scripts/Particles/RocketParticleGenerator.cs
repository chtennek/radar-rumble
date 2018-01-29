﻿using UnityEngine;using System.Collections.Generic;public class RocketParticleGenerator : MonoBehaviour {	// Constants	private const int NUM_PARTICLES = 20;	private const float TIME_BETWEEN_NEW_SPAWNS = 0.1f;    // References    public GameObject particlePrefab;    private Transform myTransform;    private Rigidbody2D myRigidBody;    private SpriteRenderer mySpriteRenderer;    // Properties    private List<Particle> _particles;    private int _currentIndex;    public float fadeOutDuration;    public float randomVariance;
    public float timeUntilNewSpawn; // Prevents particles from spawning too much    // ========================= Initialization =========================    // Use this for initialization    void Start () {		// Set references		myTransform = this.gameObject.transform;        myRigidBody = GetComponent<Rigidbody2D>();        mySpriteRenderer = GetComponent<SpriteRenderer>();    }			// ========================= Methods =========================	public void SpawnParticle(Vector3 direction) {		if (timeUntilNewSpawn <= 0f) {			// Reset spawn timer			timeUntilNewSpawn = TIME_BETWEEN_NEW_SPAWNS;

            // Turn on a particle
            GameObject newParticleObject = Instantiate(particlePrefab);            Particle newParticle = newParticleObject.GetComponent<Particle>();            newParticle.Initialize(mySpriteRenderer.color, randomVariance, fadeOutDuration, true);
            Vector3 position = new Vector3(myTransform.position.x, myTransform.position.y, -2);
            newParticle.Activate(position, direction);		}	}	void Update() {		timeUntilNewSpawn -= Time.deltaTime;        this.SpawnParticle(myRigidBody.velocity);    }}