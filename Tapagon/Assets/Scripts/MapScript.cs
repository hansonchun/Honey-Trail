using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScript : MonoBehaviour {

	public GameManager GameManager;
	public GameObject MapStartPoint;
	public Transform mapThreshold;

	public GameObject hexPrefab;
	public Sprite honeyComb;
	public Sprite emptyComb;
	public Sprite bombComb;

	// Size of the map in terms of number of hex tiles
	int width = 3; 
	int height = 50;

	float xOffset = 0.882f, yOffset = 0.764f;

	// Speed at which the map moves (difficulty)
	float speed;
	float maxSpeed;
	float acceleration = 0.03f;

	void Update() {

		transform.Translate (0, Time.deltaTime * (-speed), 0);

		RecycleMap ();

		if (speed < maxSpeed) {
			speed = speed + acceleration * Time.smoothDeltaTime;
		}
	}
		
	// Generates new set of Hex tiles
	void GenerateHex() {
		
		for (int x = 0; x < width; x++) {
			
			for (int y = 0; y < height; y++) { 
		
				float xPos = x * xOffset;

				// Check if we are on odd row
				if (y % 2 == 1) {
					xPos += xOffset/2f;
				}

				GameObject hex_go = (GameObject)Instantiate (hexPrefab, new Vector2 (gameObject.transform.localPosition.x + xPos, gameObject.transform.localPosition.y + (y * yOffset)), Quaternion.identity);

				hex_go.name = "Hex_" + x + "_" + y;

				hex_go.transform.SetParent (this.transform.FindChild("Hextiles"));

			}
		}
			
		BuildPath ();

	}

	// Destroys all hextiles
	void DestroyHex() {

		foreach (Transform child in transform.FindChild("Hextiles")) {

			Destroy (child.gameObject);
		}
			
	}

	// Builds the path
	void BuildPath() {

		Dictionary<string, List<GameObject> > hexMap = new Dictionary<string, List<GameObject> >();
		List<GameObject> path = new List<GameObject> ();
		List<GameObject> obstacles = new List<GameObject> ();

		foreach (Transform child in transform.GetChild(1)) {
				
				var rowNum = child.transform.gameObject.name.Substring (child.transform.gameObject.name.LastIndexOf ('_') + 1);
				List<GameObject> row = new List<GameObject> ();

				var wasFound = hexMap.TryGetValue (rowNum, out row);

				if (!wasFound) {
					row = new List<GameObject> ();
					hexMap.Add (rowNum, row);
				}

				row.Add (child.gameObject);

		}

		// Generate the path
		foreach (List<GameObject> row in hexMap.Values) {

			int random = Random.Range (0, width);
			GameObject floor = row [random];
			path.Add (floor);
	
			SpriteRenderer sr = floor.GetComponentInChildren<SpriteRenderer> ();
			sr.sprite = honeyComb;

		}

		// Generate the obstacles
		for (int i = 0; i < 20; i++) {

			// Do not generate an obstacle for the first 10 hextiles
			int random = Random.Range (10, path.Count);
			GameObject obstacle = path [random];

			if (!obstacles.Contains (obstacle)) {
				obstacles.Add (obstacle);
				SpriteRenderer sr = obstacle.GetComponentInChildren<SpriteRenderer> ();
				sr.sprite = bombComb;
			}


		}
	}

	void RecycleMap() {

		if (gameObject.transform.position.y < mapThreshold.transform.position.y) {

			gameObject.transform.position = MapStartPoint.transform.position;
			ResetHextiles ();
			BuildPath ();
		}
	
	}

	void ResetHextiles() {

		foreach (Transform child in transform.FindChild("Hextiles")) {

			SpriteRenderer sr = child.GetComponent<SpriteRenderer> ();
			sr.sprite = emptyComb;
			sr.material.color = Color.white;
		}
	}

	public void CheckMode(string mode) {

		if (mode.Equals ("easy")) {

			width = 3;
			speed = 1.5f;
			maxSpeed = 5.0f;
			GenerateHex ();
			Time.timeScale = 1;

		} else if (mode.Equals ("hard")) {

			width = 4;
			speed = 2.5f;
			maxSpeed = 6.0f;
			GenerateHex ();
			Time.timeScale = 1;
		}
	}

	public void StopMap() {
	
		speed = 0f;
	}
		

	public void EndMap() {
		
		DestroyHex ();

	}

	public void RestartMap(string mode) {

		CheckMode (mode);

	}
		
}
