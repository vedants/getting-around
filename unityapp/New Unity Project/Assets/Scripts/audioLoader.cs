using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniJSON;

public class audioLoader : MonoBehaviour {
	
	private string _URL = "http://localhost:5000/get_markers";
	private string _baseURL = "http://localhost:5000/get_audio/";
	public Dictionary<string, AudioClip> data; 
	public Dictionary<string, bool> visited;
	public List<AudioClip> audioClips;
	public AudioSource audio;


	void Start () {
		data = new Dictionary<string, AudioClip>(); 
		visited = new Dictionary<string, bool>(); 
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
		Debug.Log (visited);
	}
}