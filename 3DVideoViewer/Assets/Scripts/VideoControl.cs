﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VideoControl : MonoBehaviour {
	public Button pauseResumeButton;
	public GameObject startGameObject;
	public GameObject menuGameObject;
	public GameObject endVideoGameObject;
	public ParticleSystem endParticleSystem;

	public UnityEngine.Video.VideoPlayer videoPlayer1;
	public UnityEngine.Video.VideoPlayer videoPlayer2;
	private UnityEngine.Video.VideoPlayer videoPlayer;
	private bool finished = true;
	private bool started = false;


	public GameObject audioSourceObject1;
	public GameObject audioSourceObject2;
	private GvrAudioSource audioSource1;
	private GvrAudioSource audioSource2;
	private GvrAudioSource audioSource;



	void Start () {
		startGameObject.SetActive (true);
		menuGameObject.SetActive (false);
		endVideoGameObject.SetActive (false);

		audioSource1 = audioSourceObject1.GetComponent<GvrAudioSource> ();
		audioSource2 = audioSourceObject2.GetComponent<GvrAudioSource> ();

	/*	if (videoPlayer1.clip != null) 
		{
			videoPlayer1.EnableAudioTrack (0, true);
			videoPlayer1.SetTargetAudioSource(0, audioSource1);
		}

		if (videoPlayer2.clip != null) 
		{
			videoPlayer2.EnableAudioTrack (0, true);
			videoPlayer2.SetTargetAudioSource(0, audioSource2);
		}*/
	}

	//Check if input keys have been pressed and call methods accordingly.
	void Update(){
		//Play or pause the video.
		if (!finished) {
			if (Input.GetKeyDown ("space")) {
				PauseResumeVideo ();
				audioSource.Play ();
			}

			long frameCount = (long)videoPlayer.frameCount;

			if (videoPlayer.frame < frameCount) {
				started = true;
			}

			if (started && videoPlayer.frame >= frameCount) { 
				startGameObject.SetActive (false);
				menuGameObject.SetActive (false);
				endVideoGameObject.SetActive (true);
				endParticleSystem.Play ();
				finished = true;
			}
		}
	}

	public void StartVideo1(){
		startGameObject.SetActive (false);
		menuGameObject.SetActive (true);
		endVideoGameObject.SetActive (false);
		finished = false;
		started = false;
		videoPlayer = videoPlayer1;
		audioSource = audioSource1;
		this.transform.rotation = videoPlayer.transform.rotation;
		videoPlayer.Play ();
		audioSource.Play ();
	}

	public void StartVideo2(){
		startGameObject.SetActive (false);
		menuGameObject.SetActive (true);
		endVideoGameObject.SetActive (false);
		finished = false;
		started = false;
		videoPlayer = videoPlayer2;
		audioSource = audioSource2;
		this.transform.rotation = videoPlayer.transform.rotation;
		videoPlayer.Prepare ();
		videoPlayer.Play ();
		audioSource.Play ();
	}

	public void RestartVideo(){
		startGameObject.SetActive (false);
		menuGameObject.SetActive (true);
		endVideoGameObject.SetActive (false);
		finished = false;
		started = false;
		videoPlayer.Stop ();
		videoPlayer.Play ();
		audioSource.Stop ();
		audioSource.Play ();
		endParticleSystem.Stop ();
	}

	public void RestartScene(){
		SceneManager.LoadScene("Scene");
		endParticleSystem.Stop ();
	}

	public void PauseResumeVideo(){
		if (videoPlayer.isPlaying) {
			videoPlayer.Pause ();
			audioSource.Pause ();
			pauseResumeButton.GetComponentInChildren<Text> ().text = "Resume";
		}else{
			videoPlayer.Play();
			audioSource.Play ();
			pauseResumeButton.GetComponentInChildren<Text> ().text = "Pause";
		}
	}
}