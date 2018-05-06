using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class toggleModes : MonoBehaviour {
	public Text buttonText; 

	public void toggleMode() {
		buttonText.text = "changed......"; 
	}
}
