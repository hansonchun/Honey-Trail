using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HextileScript : MonoBehaviour {

	public Transform easyGameThreshold;
	public Transform hardGameThreshold;
	public Transform mapThreshold;
	public GameObject GameManager;
	public GameObject AudioManager;
	public GameObject map;

	Dictionary<string, List<GameObject> > hexMap;

	public Sprite honeyComb;
	public Sprite bombComb;

	// Use this for initialization
	void Start () {

		if (GameManager == null) {
			GameManager = GameObject.FindWithTag ("GameManager");
		}

		if (AudioManager == null) {
			AudioManager = GameObject.FindWithTag ("AudioManager");
		}

		easyGameThreshold = GameManager.transform.FindChild ("EasyGameThreshold");
		hardGameThreshold = GameManager.transform.FindChild ("HardGameThreshold");

	}
	
	// Update is called once per frame
	void Update () {

		SpriteRenderer sr = gameObject.GetComponentInChildren<SpriteRenderer> ();
		string mode = GameManager.GetComponent<GameManager> ().mode;

		if (mode.Equals ("easy")) {

			CheckThreshold (sr, easyGameThreshold);
		
		} else if (mode.Equals ("hard")) {

			CheckThreshold (sr, hardGameThreshold);
		}

	}

	void CheckThreshold(SpriteRenderer sr, Transform threshold) {

		if (sr.sprite != bombComb) {
	
			if (sr.sprite == honeyComb && sr.material.color != Color.green) {

				if (gameObject.transform.position.y < threshold.position.y) {

					if (!GameManager.GetComponent<GameManager> ().isSoundPlayed) {

						AudioManager.GetComponent<AudioManager> ().windAudio.Play ();
						GameManager.GetComponent<GameManager> ().isSoundPlayed = true;
					}

					sr.material.color = Color.red;
					StartCoroutine (GameManager.GetComponent<GameManager> ().EndGame ());
				}
			}
		}
	}
		

}
