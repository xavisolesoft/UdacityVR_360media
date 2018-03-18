using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VideoControl : MonoBehaviour {
	public Button pauseResumeButton;

	private UnityEngine.Video.VideoPlayer videoPlayer;


	[SerializeField]
	private AudioSource audioSource;



	void Start () {
		videoPlayer = GetComponent<UnityEngine.Video.VideoPlayer> ();


		if (videoPlayer.clip != null) 
		{
			videoPlayer.EnableAudioTrack (0, true);
			videoPlayer.SetTargetAudioSource(0, audioSource);
		}
	}

	//Check if input keys have been pressed and call methods accordingly.
	void Update(){
		//Play or pause the video.
		if (Input.GetKeyDown ("space")) 
		{
			PauseResumeVideo ();
			audioSource.Play();
		}

	}

	public void RestartVideo(){
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