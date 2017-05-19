using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour {

	public GameObject GameManager;

	public GameObject panelEndPoint;

	public Vector2 speed;
	public Vector2 direction;

	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (GameManager.GetComponent<GameManager> ().isGameOver == true) {

			if (transform.position.y > panelEndPoint.transform.position.y) {

				Vector3 movement = new Vector3 (0, speed.y * direction.y, 0);
				movement *= Time.deltaTime;
				transform.Translate (movement);
			
			}

		
		}

	}
}
