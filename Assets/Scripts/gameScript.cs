using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class gameScript : MonoBehaviour {

    //Variables for the core game and timer mechanics
    public int minutesPassed;
    public int secondsPassed;
    private string minuteString;
    private string secondString;
    public bool isPaused;
    public bool gameStarted;
    private bool pauseFadeAway;
    public int playerScore;
    private int displayedScore;

    //GameObjects and other elements
    public Text timeLabel;
    public Text scoreLabel;
    public Text pauseTimeLabel;
    public Text pauseScoreLabel;
    public GameObject pauseMenu;
    public GameObject countdownPlayer;
    public VideoPlayer starsAnim1;
    public VideoPlayer starsAnim2;
    public VideoPlayer countdownTimer;
    public GameObject pauseButton;

    //Variables for sorting hit detection and platforms
    public static int targetsToCalculate;
    private GameObject[] platforms;
    private float spawnRate;
    private float time;
    private float distanceFromCamera = 10f;
    private Vector3 objectPos;


    // Start is called before the first frame update
    void Start() {

        minutesPassed = 0;
        secondsPassed = 0;

        Vector3 centerPos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, distanceFromCamera));

        //Load Platforms into Array
        platforms = Resources.LoadAll<GameObject>("targets");
        // Set Spawn Rate
        spawnRate = 5.0f;

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

        if (!isPaused && !gameStarted)
        {

            time += Time.deltaTime;

            if (time > spawnRate)
            {

                print("Tried generating new platform");

                //Generate Random Number and Instantiate Associated Array Item
                int objectNum = UnityEngine.Random.Range(0, 0);
                GameObject target = Instantiate(platforms[objectNum]);
                //platform.AddComponent<yMovement>();
                //Create Random Start Positon for Object and apply to Object

                //Roll for above or below spawn
                if (UnityEngine.Random.Range(0, 2) == 1) {
                    objectPos = new Vector3((UnityEngine.Random.Range(-8, 8)), 5, 10);
                } else {
                    objectPos = new Vector3((UnityEngine.Random.Range(-8, 8)), -5, 10);
                }

                target.transform.position = objectPos;
                //Reset Time Variable
                time = 0.0f;
            }

        }

        //Test to see if the target has been tapped
        if (Input.GetMouseButtonDown(0)) {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) {
                //Target has been tapped and can be destroyed
                Destroy(hit.collider.gameObject);

                playerScore += 100 + ((minutesPassed + 1) * (secondsPassed + (minutesPassed * 60)));

            }

        }

        if (displayedScore < playerScore) {
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

        //Grant player score based on time as well
        playerScore += (minutesPassed * 20) + 20;

        //scoreLabel.text = playerScore.ToString("n0") + "pts";
        //pauseScoreLabel.text = playerScore.ToString("n0") + "pts";

    }

    public void playPause() {

        if (!isPaused){

            print("Game PAUSED!");
            starsAnim1.Pause();
            starsAnim2.Pause();
            isPaused = true;

            CancelInvoke("timerIncrement");

            // fades the image in when pausing
            StartCoroutine(FadeImage(true));
            pauseFadeAway = true;
            pauseMenu.SetActive(true);

        } else {

            print("Game RESUMED!");
            starsAnim1.Play();
            starsAnim2.Play();

            Invoke("timerIncrement", 1);

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

    IEnumerator FadeImage(bool pauseFadeAway) {

        Invoke("enableDisable", 0.4f);

        // fade out
        if (!pauseFadeAway) {
            // decreasing i
            for (float i = 0.4f; i >= 0; i -= Time.deltaTime) {
                // set to white, but with i as alpha
                pauseMenu.GetComponent<Image>().color = new Color(255, 255, 255, i);
                yield return null;
            }
        }

        // fade in
        else {
            // increasing i
            for (float i = 0; i <= 0.4f; i += Time.deltaTime) {
                // set to white, but with i as alpha
                pauseMenu.GetComponent<Image>().color = new Color(255, 255, 255, i);
                yield return null;
            }
        }
    }

}
