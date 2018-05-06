using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropMarkers : MonoBehaviour {

	private float nextActionTime = 0.0f;
	public float period = 1.0f;
	public GameObject button; 
	public GameObject soundOrb; 
	public GameObject phone; 
	public bool muteMarkers = true; 
	public GameObject parentOrb; 
	public List<GameObject> orbs; 
	public AudioSource audio; 
	public GameObject particles; 


	public Color c1 = Color.yellow;
	public Color c2 = Color.red;
	public int lengthOfLineRenderer = 2000;
	public int lineRendererIndex = 0; 


	void Start () {
		orbs = new List<GameObject> (); 

		LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
		lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
		lineRenderer.widthMultiplier = 0.01f;
		lineRenderer.positionCount = lengthOfLineRenderer;
		float alpha = 1.0f;
		Gradient gradient = new Gradient();
		gradient.SetKeys(
			new GradientColorKey[] { new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f) },
			new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
		);
		lineRenderer.colorGradient = gradient;

	}
	
	void Update () {
		muteMarkers = button.GetComponent<loadButton>().muteMarkers;
		LineRenderer lineRenderer = GetComponent<LineRenderer>();



		if (Time.time > nextActionTime) {
			nextActionTime += period;
			if (!muteMarkers) { //dropping marker mode

				lineRenderer.SetPosition(lineRendererIndex, phone.transform.position);
				lineRendererIndex++; 

				//update markers
				foreach (GameObject orb in orbs) {
					orb.GetComponent<AudioSource> ().mute = true; 
				}
				GameObject newOrb = Instantiate (soundOrb, phone.transform.position, phone.transform.rotation);
				orbs.Add (newOrb); 
				GameObject newParticles = Instantiate (particles, phone.transform.position, phone.transform.rotation);
			} else { //unmute all orbs 
				foreach (GameObject orb in orbs) {
					orb.GetComponent<AudioSource> ().mute = false; 
				}
			}
		}
	}
}
