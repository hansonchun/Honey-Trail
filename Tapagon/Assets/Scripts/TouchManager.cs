using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchManager : MonoBehaviour {

	public GameObject GameManager;
	public GameObject ScoreManager;
	public GameObject AudioManager;

	public Sprite honeyComb;
	public Sprite emptyComb;
	public Sprite bombComb;

	bool isSoundPlayed;

	// Update is called once per frame
	void Update () {

		if (Input.touchCount > 0) {
		
			if (Input.GetTouch (0).phase == TouchPhase.Began) {
				checkTouch (Input.GetTouch (0).position);
			}
		}

		 //Handles mouse input -- for testing purposes
//		RaycastHit2D hitM = Physics2D.Raycast(Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
//
//		if(hitM.collider != null) {
//			GameObject hitObject = hitM.collider.transform.gameObject;
//
//			//Debug.Log ("Hexagon: " + hitObject.name);
//
//			if (Input.GetMouseButtonDown (0)) {
//
//				if (!EventSystem.current.IsPointerOverGameObject ()) {
//
//					SpriteRenderer sr = hitObject.GetComponentInChildren<SpriteRenderer> ();
//					// If hit object is in path, OK, else, end the game
//					if (sr.sprite == honeyComb) {
//
//						sr.material.color = Color.green;
//						// Increment Score
//						ScoreManager.GetComponent<ScoreManager> ().scoreCount += 1f;
//
//					} else if (sr.sprite == emptyComb) {
//					
//						sr.material.color = Color.red;
//						Debug.Log ("END GAME");
//						GameManager.GetComponent<GameManager> ().EndGame ();
//
//					}
//				
//				}
//			}
//
//		}
	}

	void checkTouch(Vector2 pos) { 

		Vector3 worldPoint = Camera.main.ScreenToWorldPoint (pos);
		Vector2 touchPos = new Vector2 (worldPoint.x, worldPoint.y);
		RaycastHit2D hit = Physics2D.Raycast (touchPos, Vector2.zero);

		if (hit) {

			if (!EventSystem.current.IsPointerOverGameObject ()) {
				
				GameObject hitObject = hit.collider.gameObject;
				SpriteRenderer sr = hitObject.GetComponentInChildren<SpriteRenderer> ();


				if (sr.sprite == honeyComb) {

					AudioManager.GetComponent<AudioManager> ().bubbleAudio.Play ();
					// Increment Score
					sr.material.color = Color.green;
					ScoreManager.GetComponent<ScoreManager> ().scoreCount += 1f;
		

				} else if (sr.sprite == emptyComb || sr.sprite == bombComb ) { 

					AudioManager.GetComponent<AudioManager> ().windAudio.Play();
					sr.material.color = Color.red;
					StartCoroutine(GameManager.GetComponent<GameManager> ().EndGame ());
				}

			}
		}
				
	}	
		

}
