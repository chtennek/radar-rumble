using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Particle : MonoBehaviour {
    // References
    private BoxCollider2D myBoxCollider2D;
    private Rigidbody2D myRigidbody2D;
    private SpriteRenderer mySpriteRenderer;
    private Transform myTransform;
    private Color myStartColor;
    // Variables
    private Vector3 _scaleFactor = new Vector3(0.01f, 0.01f, 0.01f);
    private float _activeDuration = 2.0f;
    private float _collidableDuration = 2.0f;
    private float _fadeOutDuration = 3f;
    private float _randomVariance = 50f;
    private bool _shouldDestroy = false;
	// Properties
	private float _duration;
    private float _timestamp;

    // ========================= Initialization =========================
    // Use this for initialization
    void Start () {}

	public void Initialize(Color color, float variance, float duration, bool shouldDestroy) {
		// Cache references
		myBoxCollider2D = GetComponent<BoxCollider2D>();
		myRigidbody2D = GetComponent<Rigidbody2D>();
		mySpriteRenderer = GetComponent<SpriteRenderer>();
        mySpriteRenderer.color = color;
        myStartColor = color;
        myTransform = this.gameObject.transform;
        _randomVariance = variance;
        _fadeOutDuration = duration;
        _shouldDestroy = shouldDestroy;
        this.gameObject.SetActive(false);

    }
	
	// ========================= Update =========================
	public void Activate (Vector3 position, Vector3 direction) {
        // Keep track of time
        _timestamp = Time.time;

		// Enable the particle
		_duration = _activeDuration;
		this.gameObject.SetActive(true);

		// Reset particle
		myBoxCollider2D.enabled = false;
        myTransform.localScale = new Vector3(1f, 1f, 1f);

        // Set particles position and direction
        myTransform.position = position;
		myTransform.rotation = Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f));
		myRigidbody2D.AddForce(new Vector3((direction.x + Random.Range(-_randomVariance, _randomVariance)) * -0.5f,
		                                   (direction.y + Random.Range(-_randomVariance, _randomVariance)) * -0.5f, 0f));
	}

	void Update() {
        // Exit condition
        if (this.gameObject == null) { return; }

        // If particle is active, slowly fade out
        if (_duration >= 0) {
			_duration -= Time.deltaTime;
            myTransform.localScale = myTransform.localScale + _scaleFactor;
            mySpriteRenderer.color = Color.Lerp(myStartColor, new Color(1, 1, 1, 0f), (Time.time - _timestamp) / _fadeOutDuration);

            // If particle has disappeared
            if (_duration <= 0) {
                if (this._shouldDestroy) {
                    Destroy(this.gameObject);
                }
                else {
                    this.gameObject.SetActive(false);
                }
			}
			else if (_duration <= _collidableDuration) {
				myBoxCollider2D.enabled = true;
			}
		}
	}
}
