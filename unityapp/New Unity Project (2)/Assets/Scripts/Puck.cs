using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puck : MonoBehaviour {

	private GameObject score;
	private Rigidbody rb;
	private Vector3 direction;
	private float speed;

	// Use this for initialization
	void Start () {
		score = GameObject.FindWithTag ("Score");
		speed = 500.0f;
		rb = GetComponent<Rigidbody> ();
		direction = (Random.value > 0.5f) ? -transform.right + (transform.forward * 0.25f) : transform.right - (transform.forward * 0.25f);
		rb.AddForce (direction * speed);
	}

	// Update is called once per frame
	void Update () {
	}

	void OnCollisionEnter (Collision col) {
		string name = col.gameObject.name;
		string tag = col.gameObject.tag;
		if (name == "Border") {
			if (tag == "PlayerGoal") {
				score.GetComponent<ScoreUI> ().scorePlayer += 1;
				direction = transform.right - (transform.forward * 0.25f);
			}
			if (tag == "PlayerAIGoal") {
				score.GetComponent<ScoreUI> ().scorePlayerAI += 1;
				direction = -transform.right + (transform.forward * 0.25f);
			}
			reset ();
		}
	}

	void reset() {
		rb.velocity = Vector3.zero;
		rb.position = new Vector3 (0f, 1f, 0f);
		rb.AddForce (direction * speed);
	}
}
