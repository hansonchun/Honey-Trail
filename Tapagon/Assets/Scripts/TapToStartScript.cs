using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapToStartScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		// Hide Tap to Start Text component
		if (Time.timeScale == 1) {

			gameObject.SetActive (false);
		}
		
	}
}
