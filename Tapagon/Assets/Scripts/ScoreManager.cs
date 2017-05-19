using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;
using System;

public class ScoreManager : MonoBehaviour {

	public GameObject GameManager;

	public Text scoreText;
	public Text restartText;
	public Text highscoreText;
	public Text modeText;

	public Button restartButton;
	public Button achievementButton;
	public Button leaderboardButton;

	public float scoreCount;

	public float HighestScore;

	// Use this for initialization
	void Start () {

		PlayGamesPlatform.Activate ();
		PlayGamesPlatform.DebugLogEnabled = true;

		if (!Social.localUser.authenticated) {
			Social.localUser.Authenticate ((bool success) => {

			});
		}

		// Instantiate buttons
		Button rBtn = restartButton.GetComponent<Button> ();
		Button aBtn = achievementButton.GetComponent<Button> ();
		Button lBtn = leaderboardButton.GetComponent<Button> ();

		rBtn.onClick.AddListener (RestartGame);
		aBtn.onClick.AddListener (toAchievement);
		lBtn.onClick.AddListener (toLeaderboard);

		//HighestScore = PlayerPrefs.GetFloat ("HighestScore");

	}
	
	// Update is called once per frame
	void Update () {

		scoreText.text = scoreCount.ToString();

		if (GameManager.GetComponent<GameManager> ().mode.Equals ("easy")) {

			highscoreText.text = "best: " + PlayerPrefs.GetFloat ("HighscoreEasy");
			modeText.text = "easy mode";
		
		} else if (GameManager.GetComponent<GameManager> ().mode.Equals ("hard")) {
		
			highscoreText.text = "best: " + PlayerPrefs.GetFloat ("HighscoreHard");
			modeText.text = "hard mode";
		}

	}

	void RestartGame() {

		GameManager.GetComponent<GameManager> ().RestartGame ();
	}

	void CheckAchievements() {
	
		if (GameManager.GetComponent<GameManager> ().mode.Equals ("easy")) {

			//Beginner beekeeper (50 drops easy mode)
			if (scoreCount >= 50f) {

				Social.ReportProgress (HoneyTrailResources.achievement_beginner_beekeeper, 100.0f, (bool success) => {
				});
			}

			if (scoreCount >= 100f) {
			
				Social.ReportProgress (HoneyTrailResources.achievement_journeyman_beekeeper, 100.0f, (bool success) => {
				});
			}

			if (scoreCount >= 200f) {
			
				Social.ReportProgress (HoneyTrailResources.achievement_skilled_beekeeper, 100.0f, (bool success) => {
				});
			}
				
		
		} else if (GameManager.GetComponent<GameManager> ().mode.Equals ("hard")) {

			if (scoreCount >= 100f) {
			
				Social.ReportProgress (HoneyTrailResources.achievement_master_beekeeper, 100.0f, (bool success) => {
				});
			}

			if (scoreCount >= 200f) {
			
				Social.ReportProgress (HoneyTrailResources.achievement_legendary_beekeeper, 100.00f, (bool success) => {
				});
			}
				
		}
	}

	public void EndScore() {

		restartText.text = scoreCount.ToString ();

		CheckAchievements ();

		if (GameManager.GetComponent<GameManager> ().mode.Equals ("easy")) {

			// Update easy highscore
			if (PlayerPrefs.GetFloat ("HighscoreEasy") < scoreCount) {

				PlayerPrefs.SetFloat ("HighscoreEasy", scoreCount);

				Social.ReportScore (Convert.ToInt64 (scoreCount), HoneyTrailResources.leaderboard_most_honey_collected__easy_mode, (bool success) => {
				});
			}
				
		} else if (GameManager.GetComponent<GameManager> ().mode.Equals ("hard")) {
		
			// Update hard highscore
			if (PlayerPrefs.GetFloat ("HighscoreHard") < scoreCount) {

				PlayerPrefs.SetFloat ("HighscoreHard", scoreCount);

				Social.ReportScore (Convert.ToInt64 (scoreCount), HoneyTrailResources.leaderboard_most_honey_collected__hard_mode, (bool success) => {
				});
			}
		
		}

			
	}

	public void RestartScore() {

		scoreCount = 0f;
	}

	public void toAchievement() {

		if (Social.localUser.authenticated) {
			Social.ShowAchievementsUI ();
		}
	}

	public void toLeaderboard() {
		if (Social.localUser.authenticated) {
			Social.ShowLeaderboardUI ();
		}
	
	}
		
}
