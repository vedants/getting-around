using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioExperiments : MonoBehaviour {

	// Use this for initialization
	public AudioClip audioClip;
	public AudioSource audioSource;
	public GameObject player;
	float RADIUS = 3;

	private float[] sample = new float[256];

	void Start () {
		audioSource.Play ();

	}
	
	void Update () {
		float distance = Vector3.Distance (transform.position, player.transform.position);
//		if (distance > RADIUS) {
//			audioSource.volume = 0;
//		} else {
//			audioSource.volume = (float)(1 / (distance + 0.001));
//		}

		var ac = audioSource;

		if (Input.GetMouseButton(0))
		{
			ac.pitch = 2.0f;
		}
		//loop around audio. 
		if (ac.time > ac.clip.length)
		{
			ac.time = 0.0f;
		}

		if (Input.GetMouseButtonUp(0))
		{
			ac.time = ac.time * 1.41f;
			ac.pitch = 1.0f;
			Debug.Log(ac.time);
			Debug.Log(ac.timeSamples);
		}

	}
}
