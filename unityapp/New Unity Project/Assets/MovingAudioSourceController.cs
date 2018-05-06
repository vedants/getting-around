using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingAudioSourceController : MonoBehaviour {

	public GameObject phone; 
	public AudioSource audioSource; 



	private Vector3 OFFSET = new Vector3(2,0,1);
	private Vector3 previousPosition; 
	void Start () {
		previousPosition = transform.position;
		
	}
	
	// Update is called once per frame
	void Update () {
		//follow the phonea at some offset. 
		transform.position = Vector3.MoveTowards(transform.position, phone.transform.position + OFFSET, .3f);

		//velocity calculation probably would benefit from some kind of smoothing 
		//maybe just stick a rigibbody on it? 
		Vector3 velocity = (previousPosition - transform.position) / Time.deltaTime; 
		if (velocity.magnitude > 0) {
			audioSource.pitch = audioSource.pitch += Mathf.Log10(velocity.magnitude);
		} else {
			audioSource.pitch = 1;
		}

		previousPosition = transform.position;
	}
}
