using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Particle : MonoBehaviour {
    // Variables
    public float activeDuration = 1.0f;
    public float collidableDuration = 0.8f;
	public float fadeOutDuration = 0.4f;
	// References
	private BoxCollider2D myBoxCollider2D;
	private Rigidbody2D myRigidbody2D;
	private SpriteRenderer mySpriteRenderer;
	private Transform myTransform;
	// Properties
	private float _activeDuration;
	private Color _fadeOutColor = new Color(1f, 1f, 1f, 0.3f);
	
	// ========================= Initialization =========================
	// Use this for initialization
	void Start () {}

	public void Initialize(Color color) {
		// Cache references
		myBoxCollider2D = GetComponent<BoxCollider2D>();
		myRigidbody2D = GetComponent<Rigidbody2D>();
		mySpriteRenderer = GetComponent<SpriteRenderer>();
		myTransform = this.gameObject.transform;
        mySpriteRenderer.color = color;
        this.gameObject.SetActive(false);
	}
	
	// ========================= Update =========================
	public void Activate (Vector3 position, Vector3 direction) {
		// Enable the particle
		_activeDuration = activeDuration;
		this.gameObject.SetActive(true);

		// Reset particle
		myBoxCollider2D.enabled = false;

		// Set particles position and direction
		myTransform.position = position;
		myTransform.rotation = Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f));
		myRigidbody2D.AddForce(new Vector3((direction.x + Random.Range(-1.2f, 1.2f)) * -0.5f,
		                                   (direction.y + Random.Range(-1.2f, 1.2f)) * -0.5f, 0f));
	}

	void Update() {
		// If particle is active, slowly fade out
		if (_activeDuration >= 0) {
			_activeDuration -= Time.deltaTime;

			if (_activeDuration <= 0) {
				this.gameObject.SetActive(false);
			}
			else if (_activeDuration <= fadeOutDuration) {
				mySpriteRenderer.color = _fadeOutColor;
			}
			else if (_activeDuration <= collidableDuration) {
				myBoxCollider2D.enabled = true;
			}
		}
	}
}
