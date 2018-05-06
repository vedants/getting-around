using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MiniJSON;

public class GeoLocate : MonoBehaviour {


	public float period = 1.0f; //how often to update distances
	public float RADIUS = 10f; //in meters, how close before audio starts playing
	private float nextActionTime = 0.0f; 

	private Dictionary<string, AudioClip> data = new Dictionary<string, AudioClip>(); //maps coord to audio clip
	private Dictionary<string, bool> visited = new Dictionary<string, bool>(); 


	private string _URL = "http://localhost:5000/get_markers";
	private string _baseURL = "http://localhost:5000/get_audio/";
	public List<AudioClip> audioClips;
	public AudioSource audio;

	public GameObject person; 
	private AudioSource speaker; 
	public GameObject btn; 

	void Start () {
		Input.location.Start();
		speaker = person.GetComponent<AudioSource> (); 
		WWW www = new WWW(_URL);
		StartCoroutine(GetAudioNames(www));
	}

	IEnumerator GetAudioNames(WWW www)
	{
		yield return www;
		if (www.error == null)
		{
			Dictionary<string,object> dict = new Dictionary<string,object>();
			dict = Json.Deserialize(www.text) as Dictionary<string,object>;
			Debug.Log (dict);
			foreach (KeyValuePair<string, object> marker in dict) {
				getAudio (marker.Key, _baseURL + marker.Value);
			}
		} else {
			Debug.Log("WWW Error: "+ www.error);
		}    
	}

	void getAudio(string coord, string url) {
		StartCoroutine (fetchAudioFromURL (coord, url));
	}

	IEnumerator fetchAudioFromURL(string coord, string url) {
		WWW www = new WWW(url);
		yield return www;
		data[coord] = www.GetAudioClip(false, false);
		visited[coord] = false;
	}
	
	void Update () {
		if (Time.time >= nextActionTime) {
			nextActionTime += period;	

			// First, check if user has location services enabled
			if (!Input.location.isEnabledByUser) {
				Debug.Log ("Location not enabled. "); 
			}
				
			foreach (KeyValuePair<string, AudioClip> marker in data) {

				string[] latlong = marker.Key.Split('_'); 
				float lat = float.Parse (latlong [0]);
				float lng = float.Parse (latlong [1]);
				float dist = Calc (lat, lng, Input.location.lastData.latitude, Input.location.lastData.longitude); 
				if (dist <= RADIUS) {
					if (! visited [marker.Key]) {
						AudioClip audioclip = marker.Value; 
						//play audio 
						speaker.clip = audioclip; 
						speaker.Play(); 	
						visited [marker.Key] = true; 
					}
					visited [marker.Key] = false; 
				}
			}
		}
	}
		
    public float Calc(float lat1, float lon1, float lat2, float lon2)
    //Returned a distance in meters between two lat,long coordinates using the Haversine approximation equation 
	{       
         float R = 6378.137f; // Radius of earth in KM
         float dLat = lat2 * Mathf.PI / 180 - lat1 * Mathf.PI / 180;
         float dLon = lon2 * Mathf.PI / 180 - lon1 * Mathf.PI / 180;
         float a = Mathf.Sin(dLat / 2) * Mathf.Sin(dLat / 2) +
         Mathf.Cos(lat1 * Mathf.PI / 180) * Mathf.Cos(lat2 * Mathf.PI / 180) *
         Mathf.Sin(dLon / 2) * Mathf.Sin(dLon / 2);
         float c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));
         float  d = R * c;
         return d * 1000f; // meters
     }		
}