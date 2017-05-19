using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using admob;

public class AdManager : MonoBehaviour {

	public static AdManager Instance{ set; get; }

	public string bannerId;
	public string videoId;

	private void Start() {

		Instance = this;
		DontDestroyOnLoad (gameObject);

		#if UNITY_EDITOR
		#elif UNITY_ANDROID
		Admob.Instance ().initAdmob (bannerId, videoId);
		#endif
	}

	public void ShowBanner() {

		#if UNITY_EDITOR
		#elif UNITY_ANDROID
		Admob.Instance ().showBannerRelative (AdSize.Banner, AdPosition.BOTTOM_CENTER, 5);
		#endif
	}
}
