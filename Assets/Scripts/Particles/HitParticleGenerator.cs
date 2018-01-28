using UnityEngine;using System.Collections.Generic;public class HitParticleGenerator : MonoBehaviour {
    // Constants
    private const int NUM_PARTICLES = 8;
    // References
    public GameObject particlePrefab;    private Transform myTransform;    private Rigidbody2D myRigidBody;
    // Properties
    private List<Particle> _particles;
    public float fadeOutDuration;    public float randomVariance;

    // ========================= Initialization =========================
    // Use this for initialization
    void Start() {
        // Set references
        myTransform = this.gameObject.transform;        myRigidBody = GetComponent<Rigidbody2D>();        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        // Create pool of particles        _particles = new List<Particle>();        for (int i = 0; i < NUM_PARTICLES; ++i) {            GameObject newParticleObject = Instantiate(particlePrefab);            Particle newParticle = newParticleObject.GetComponent<Particle>();            newParticle.Initialize(spriteRenderer.color, fadeOutDuration, randomVariance);            _particles.Add(newParticle);        }    }

    // ========================= Methods =========================
    public void SpawnParticles() {
        // Turn on particles
        for (int i=0; i< NUM_PARTICLES; ++i) {
            _particles[i].Activate(myTransform.position,
                new Vector3(Random.Range(0f, 360f), 1f, 0));
        }
    }}