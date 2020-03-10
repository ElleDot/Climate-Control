using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class gameScript : MonoBehaviour {

    private int minutesPassed;
    private int secondsPassed;
    private string minuteString;
    private string secondString;
    public bool isPaused;
    public bool gameStarted;
    private bool pauseFadeAway;
    private int playerScore;
    private int displayedScore;

    public Text timeLabel;
    public Text scoreLabel;
    public Text pauseTimeLabel;
    public Text pauseScoreLabel;
    public GameObject pauseMenu;
    public GameObject countdownPlayer;
    public VideoPlayer starsAnim1;
    public VideoPlayer starsAnim2;
    public VideoPlayer countdownTimer;
    

    // Start is called before the first frame update
    void Start() {

        minutesPassed = 0;
        secondsPassed = 0;
        Invoke("countdownStart", 1);

    }

    // Called when the countdown video finishes playing
    public void CheckOver(UnityEngine.Video.VideoPlayer vp) {

        timerIncrement();
        CancelInvoke("checkOver");
        countdownPlayer.SetActive(false);

    }

    // Begins the countdown timer
    public void countdownStart() {

        countdownTimer.playbackSpeed = 1;
        //Invoke repeating of checkOver method
        countdownTimer.loopPointReached += CheckOver;

    }

    void Update() {

        if (displayedScore < playerScore)
        {
            displayedScore++;
            if (displayedScore > playerScore) {
                displayedScore = playerScore;
            }
                
        }

        scoreLabel.text = displayedScore.ToString("n0") + "pts";
        pauseScoreLabel.text = displayedScore.ToString("n0") + "pts";

    }

    public void timerIncrement() {

        Invoke("timerIncrement", 1);

        if (!isPaused) {

            if (secondsPassed < 59) {
                secondsPassed += 1;
            } else {
                minutesPassed += 1;
                secondsPassed = 0;
            }

            if (minutesPassed < 10) {
                minuteString = "0" + minutesPassed.ToString();
            } else {
                minuteString = minutesPassed.ToString();
            }

            if (secondsPassed < 10) {
                secondString = "0" + secondsPassed.ToString("f0");
            } else {
                secondString = secondsPassed.ToString("f0");
            }

            timeLabel.text = minuteString + ":" + secondString;
            pauseTimeLabel.text = minuteString + ":" + secondString;

            playerScore += (minutesPassed * 20) + 20;
        }

        //scoreLabel.text = playerScore.ToString("n0") + "pts";
        //pauseScoreLabel.text = playerScore.ToString("n0") + "pts";

    }

    public void playPause() {

        if (!isPaused){

            print("Game PAUSED!");
            starsAnim1.Pause();
            starsAnim2.Pause();
            isPaused = true;

            // fades the image in when pausing
            StartCoroutine(FadeImage(true));
            pauseFadeAway = true;
            pauseMenu.SetActive(true);

        } else {

            print("Game RESUMED!");
            starsAnim1.Play();
            starsAnim2.Play();

            // fades the image out when resuming
            StartCoroutine(FadeImage(false));
            pauseFadeAway = false;

        }

    }

    public void enableDisable() {
        if (!pauseFadeAway) {
            pauseMenu.SetActive(false);
            isPaused = false;
        }
    }

    IEnumerator FadeImage(bool pauseFadeAway)
    {

        Invoke("enableDisable", 0.4f);

        // fade out
        if (!pauseFadeAway) {
            // decreasing i
            for (float i = 0.4f; i >= 0; i -= Time.deltaTime) {
                // set color with i as alpha
                pauseMenu.GetComponent<Image>().color = new Color(255, 255, 255, i);
                yield return null;
            }
        }

        // fade in
        else {
            // increasing i
            for (float i = 0; i <= 0.4f; i += Time.deltaTime) {
                // set color with i as alpha
                pauseMenu.GetComponent<Image>().color = new Color(255, 255, 255, i);
                yield return null;
            }
        }
    }

}
