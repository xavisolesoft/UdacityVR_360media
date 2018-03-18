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

	public UnityEngine.Video.VideoPlayer videoPlayer1;
	public UnityEngine.Video.VideoPlayer videoPlayer2;
	private UnityEngine.Video.VideoPlayer videoPlayer;
	private bool finished = true;


	[SerializeField]
	private AudioSource audioSource;



	void Start () {
		startGameObject.SetActive (true);
		menuGameObject.SetActive (false);
		endVideoGameObject.SetActive (false);

		videoPlayer1 = GetComponents<UnityEngine.Video.VideoPlayer> ()[0];
		videoPlayer2 = GetComponents<UnityEngine.Video.VideoPlayer> ()[1];

		if (videoPlayer1.clip != null) 
		{
			videoPlayer1.EnableAudioTrack (0, true);
			videoPlayer1.SetTargetAudioSource(0, audioSource);
		}
	}

	//Check if input keys have been pressed and call methods accordingly.
	void Update(){
		//Play or pause the video.
		if (!finished) {
			if (Input.GetKeyDown ("space")) {
				PauseResumeVideo ();
				audioSource.Play ();
			}

			if (videoPlayer.frame >= (long)videoPlayer.frameCount) { 
				menuGameObject.SetActive (false);
				endVideoGameObject.SetActive (true);
				finished = true;
			}
		}
	}

	public void StartVideo1(){
		startGameObject.SetActive (false);
		menuGameObject.SetActive (true);
		endVideoGameObject.SetActive (false);
		finished = false;
		videoPlayer = videoPlayer1;
		videoPlayer.Play ();
	}

	public void StartVideo2(){
		startGameObject.SetActive (false);
		menuGameObject.SetActive (true);
		endVideoGameObject.SetActive (false);
		finished = false;
		videoPlayer = videoPlayer2;
		videoPlayer.Play ();
	}

	public void RestartVideo(){
		startGameObject.SetActive (false);
		menuGameObject.SetActive (true);
		endVideoGameObject.SetActive (false);
		finished = false;
		videoPlayer.Play ();
	}

	public void RestartScene(){
		SceneManager.LoadScene("Scene");
	}

	public void PauseResumeVideo(){
		if (videoPlayer.isPlaying) {
			videoPlayer.Pause ();
			pauseResumeButton.GetComponentInChildren<Text> ().text = "Resume";
		}else{
			videoPlayer.Play();
			pauseResumeButton.GetComponentInChildren<Text> ().text = "Pause";
		}
	}
}