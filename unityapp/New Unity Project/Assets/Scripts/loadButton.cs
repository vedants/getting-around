using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class loadButton : MonoBehaviour {
	public Text t; 
	public bool muteMarkers; 

	void Start () {
		Button btn = this.GetComponent<Button>();
		btn.onClick.AddListener(ToggleMode);
	}
	
	void ToggleMode () {
		muteMarkers = ! muteMarkers; 
		if (!muteMarkers) {
			t.text = "Dropping breadcrumbs..."; 
		} else {
			t.text = "Retracing breadcrumbs..."; 
		}
		
	}
}
