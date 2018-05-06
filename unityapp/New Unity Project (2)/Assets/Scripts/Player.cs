using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	private float value;
	private GameObject player;
	private bool[] isColliding;
	private float height;

	// Use this for initialization
	void Start () {
		player = gameObject;
		isColliding = new bool[2];
		isColliding [0] = false;
		isColliding [1] = false;
	}
	
	// Update is called once per frame
	void Update () {
		height = player.transform.position.z;
		value = Input.GetAxis ("Vertical");
		if (value > 0 && !isColliding [0]) {
			player.transform.position += Vector3.back * (1 + Time.smoothDeltaTime);
			isColliding [1] = false;
		} else if (value < 0 && !isColliding [1]) {
			player.transform.position += Vector3.forward * (1 + Time.smoothDeltaTime);
			isColliding [0] = false;
		}
	}

}
