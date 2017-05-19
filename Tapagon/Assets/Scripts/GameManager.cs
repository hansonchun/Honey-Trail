using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;

public class GameManager : MonoBehaviour {

	public bool isFirstRun = true;
	public bool isGameOver = true;
	public string mode;

	public GameObject map1;
	public GameObject map2;
	private Vector3 map1StartPoint;
	private Vector3 map2StartPoint;

	public GameObject ScoreManager;
	public Text scoreText;
	public RawImage honeyDrop;

	public Canvas canvas;
	public GameObject gameOverPanel;
	public GameObject easyPanelStartPoint;
	public GameObject hardPanelStartPoint;

	public GameObject levelPanel;
	public GameObject easyButton;
	public GameObject hardButton;
	public GameObject mainMenuButton;

	public bool isSoundPlayed;

	// Use this for initialization
	void Start () {

		Time.timeScale = 0;

		map1StartPoint = map1.transform.position;
		map2StartPoint = map2.transform.position;

		gameOverPanel.transform.position = easyPanelStartPoint.transform.position;

		// Instantiate Buttons
		Button eBtn = easyButton.GetComponent<Button>();
		Button hBtn = hardButton.GetComponent<Button> ();
		Button mmBtn = mainMenuButton.GetComponent<Button> ();
		eBtn.onClick.AddListener (setEasyMode);
		hBtn.onClick.AddListener (setHardMode);
		mmBtn.onClick.AddListener (toMainMenu);

		// Hide scores
		scoreText.enabled = false;
		honeyDrop.enabled = false;

		isSoundPlayed = false;

	}
		
	void setEasyMode() {

		mode = "easy";
		AdjustCamera (1.135f, 2.46f);
		StartGame ();
	}

	void setHardMode() {

		mode = "hard";
		AdjustCamera (1.57f, 3.45f);
		StartGame ();
	}

	void toMainMenu() {

		Application.LoadLevel (Application.loadedLevel);
	}

	void StartGame() {

		levelPanel.SetActive (false);
		isGameOver = false;
		scoreText.enabled = true;
		honeyDrop.enabled = true;

		map1.GetComponent<MapScript> ().CheckMode (mode);
		map2.GetComponent<MapScript> ().CheckMode (mode);
	}

	void AdjustCamera(float x, float size) {

		Vector3 pos = Camera.main.transform.position;
		pos.x = x;

		Camera.main.transform.position = pos;
		Camera.main.orthographicSize = size;	
	}
		
	public IEnumerator EndGame() {
		// Stop the map from moving
		map1.GetComponent<MapScript> ().StopMap ();
		map2.GetComponent<MapScript> ().StopMap ();

		scoreText.enabled = false;
		honeyDrop.enabled = false;

		yield return new WaitForSeconds (0.5f);
		isGameOver = true;
		map1.GetComponent<MapScript> ().EndMap ();
		map2.GetComponent<MapScript> ().EndMap ();
		ScoreManager.GetComponent<ScoreManager> ().EndScore ();
		AdManager.Instance.ShowBanner ();
	}
		
	public void RestartGame() {

		isSoundPlayed = false;
		isGameOver = false;

		scoreText.enabled = true;
		honeyDrop.enabled = true;

		// Reposition map and game over panel
		map1.transform.position = map1StartPoint;
		map2.transform.position = map2StartPoint;

		if (mode.Equals ("easy")) {
			gameOverPanel.transform.position = easyPanelStartPoint.transform.position;
		} else if (mode.Equals ("hard")) {
			gameOverPanel.transform.position = hardPanelStartPoint.transform.position;
		}

		// Restart map and score manager
		map1.GetComponent<MapScript> ().RestartMap (mode);
		map2.GetComponent<MapScript> ().RestartMap (mode);
		ScoreManager.GetComponent<ScoreManager> ().RestartScore ();
	}

}
