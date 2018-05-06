using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAI : MonoBehaviour {

	private float value;
	private GameObject player;
	private bool[] isColliding;
	private float height;
	private float puckHeight;
	private float tolerance;

	// Use this for initialization
	void Start () {
		player = gameObject;
		isColliding = new bool[2];
		isColliding [0] = false;
		isColliding [1] = false;
		tolerance = 0.5f;
	}

	// Update is called once per frame
	void Update () {
		height = player.transform.position.z;
		puckHeight = GameObject.FindWithTag ("Puck").transform.position.z;
		if (height < (puckHeight - tolerance)) {
			up ();
		} else if (height > (puckHeight + tolerance)) {
			down ();
		}
	}

	void up () {
		if (height + Vector3.forward.z * (1 + Time.smoothDeltaTime) >= 5) {
			isColliding [1] = true;
		}
		if (!isColliding [1]) {
			player.transform.position += Vector3.forward * (0.2f + Time.smoothDeltaTime);
			isColliding [0] = false;
		}
	}

	void down () {
		if (height + Vector3.back.z * (1 + Time.smoothDeltaTime) <= -5) {
			isColliding [0] = true;
		}
		if (!isColliding [0]) {
			player.transform.position += Vector3.back * (0.2f + Time.smoothDeltaTime);
			isColliding [1] = false;
		}
	}

	void OnCollisionEnter (Collision col) {
		if (col.gameObject.name == "Border-Top") {
			isColliding [0] = true;
		} else if (col.gameObject.name == "Border-Bottom") {
			isColliding [1] = true;
		}
	}
}
