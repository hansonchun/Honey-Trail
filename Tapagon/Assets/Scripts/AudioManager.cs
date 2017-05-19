using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public AudioClip bubbleClip;
	public AudioClip windClip;

	public AudioSource bubbleAudio;
	public AudioSource windAudio;

	public AudioSource AddAudio(AudioClip clip, bool loop, bool playAwake, float vol) {
	
		AudioSource newAudio = gameObject.AddComponent<AudioSource> ();
		newAudio.clip = clip;
		newAudio.loop = loop;
		newAudio.playOnAwake = playAwake;
		newAudio.volume = vol;
		return newAudio;
	}

	void Awake() {
	
		bubbleAudio = AddAudio (bubbleClip, false, false, 1);
		windAudio = AddAudio (windClip, false, false, 0.5f);
	}
}
