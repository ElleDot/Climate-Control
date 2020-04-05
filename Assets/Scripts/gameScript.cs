using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class gameScript : MonoBehaviour {

    //Variables for the core game and timer mechanics
    public int minutesPassed;
    public int secondsPassed;
    public string minuteString;
    public string secondString;
    public static bool isPaused;
    public bool gameStarted = false;
    public bool pauseFadeAway;
    public bool pauseButtonFadeAway;
    public int playerScore;
    public int displayedScore;
    public int livesLeft;
    public int goodObjects;
    public int maxCombo;
    public int currentCombo;
    public int targetsDestroyed;

    //Sounds for the game
    public AudioSource goodHitSound;
    public AudioSource badHitSound;
    public AudioSource audioSource;

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
    public Button pauseButton;
    public GameObject lifeOneIcon;
    public GameObject lifeTwoIcon;
    public GameObject lifeThreeIcon;
    public Sprite pauseButtonSprite;
    public Sprite playButtonSprite;
    public Image playPauseImage;
    public Image FadeView;
    public Image Earth;
    public Sprite HealthyEarth;
    public Sprite OKEarth;
    public Sprite UnhealthyEarth;
    public Text alertText;

    //Variables for sorting hit detection and platforms
    public GameObject[] targets;
    public float spawnRate;
    public float time;
    public Vector3 objectPos;

    // Start is called before the first frame update
    void Start() {

        minutesPassed = 0;
        secondsPassed = 0;
        livesLeft = 3;
        currentCombo = 0;
        maxCombo = 0;
        targetsDestroyed = 0;

        pauseButton.interactable = false;
        playPauseImage = GetComponent<Image>();

        //Load Platforms into Array
        targets = Resources.LoadAll<GameObject>("targets");
        // Set Spawn Rate
        spawnRate = 2.0f;

        Invoke("countdownStart", 1);

    }

    // Called when the countdown video finishes playing
    public void CheckOver(UnityEngine.Video.VideoPlayer vp) {

        timerIncrement();
        CancelInvoke("checkOver");
        countdownPlayer.SetActive(false);
        StartCoroutine(FadeButton(true));
        pauseButton.interactable = true;
        gameStarted = true;

    }

    // Begins the countdown timer
    public void countdownStart() {

        countdownTimer.playbackSpeed = 1;
        //Invoke repeating of checkOver method
        countdownTimer.loopPointReached += CheckOver;

    }

    void Update() {

        if (!isPaused && gameStarted) {

            time += Time.deltaTime;

            if (time > spawnRate) {

                print("Tried generating new target");

                //Roll for left or right spawn, then
                //create random start positon for Object and apply to Object
                if (UnityEngine.Random.Range(0, 2) == 1) {
                    objectPos = new Vector3(-10, UnityEngine.Random.Range(-10, 10), 10);
                } else {
                    objectPos = new Vector3(10, UnityEngine.Random.Range(-10, 10), 10);
                }

                //Generate Random Number and Instantiate Associated Array Item
                int objectNum = UnityEngine.Random.Range(0, 7);
                GameObject target = Instantiate(targets[objectNum]);

                //platform.AddComponent<yMovement>();
                target.transform.position = objectPos;

                //Reset Time Variable
                time = 0.0f;
            }

            if (!isPaused) {

                //Test to see if the target has been tapped
                if (Input.GetMouseButtonDown(0)) {

                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit)) {

                        targetBehaviour target = hit.collider.GetComponent<targetBehaviour>();

                        if (target != null) {

                            //Check target type here
                            if (target._targetType == TargetType.GOOD) {
                                goodTargetTapped();
                            } else {
                                badTargetTapped();
                            }

                        }

                        //Target has been tapped and can be destroyed
                        Destroy(hit.collider.gameObject);

                    }
                }

            }

        }

        if (displayedScore + 1000 < playerScore) {
            displayedScore += 250;
        } else if (displayedScore + 100 < playerScore) {
            displayedScore += 25;
        } else if (displayedScore < playerScore) {
            displayedScore++;
        } else if (displayedScore > playerScore) {
            displayedScore -= 1; 
        }

        scoreLabel.text = displayedScore.ToString("n0") + "pts";
        pauseScoreLabel.text = playerScore.ToString("n0") + "pts";

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

        //Decrease the duration between spawns every 10s
        if (secondsPassed % 5 == 0) {
            print("DIFFICULTY INCREASED!");
            if (spawnRate > 0.2f) {
                spawnRate -= 0.1f;
            }
        }

    }

    public void playPause() {

        if (!isPaused) {

            print("Game PAUSED!");
            starsAnim1.Pause();
            starsAnim2.Pause();
            isPaused = true;

            playPauseImage.sprite = playButtonSprite;

            CancelInvoke("timerIncrement");

            // fades the image in when pausing
            StartCoroutine(FadeImage(true));
            pauseFadeAway = true;
            pauseMenu.SetActive(true);

        } else {

            print("Game RESUMED!");
            starsAnim1.Play();
            starsAnim2.Play();

            playPauseImage.sprite = pauseButtonSprite;

            Invoke("timerIncrement", 1);

            // fades the image out when resuming
            StartCoroutine(FadeImage(false));
            pauseFadeAway = false;

        }

    }

    //Called after the pause menu has either been called or dismissed
    public void enableDisable() {

        if (!pauseFadeAway) {
            pauseMenu.SetActive(false);
            isPaused = false;
        }

    }

    public void goodObjectHit() {

        //Add 1 to good objects
        //Add 1 to current combo, checking to see if it's the highest

        goodObjects++;
        currentCombo++;
        if (currentCombo > maxCombo) {
            maxCombo++;
        }

        if (currentCombo % 10 == 0) {
            alertText.text = currentCombo.ToString("f0") + " target streak!";
            StopCoroutine(alertFade(true));
            StartCoroutine(alertFade(true));
        }

        //Check if that was the 10th good object
        if (goodObjects % 10 == 0) {

            if (livesLeft < 3) {

                livesLeft++;

                alertText.text = "1UP!";
                StopCoroutine(alertFade(true));
                StartCoroutine(alertFade(true));

            }

            if (livesLeft == 3) {
                lifeThreeIcon.GetComponent<Image>().color = new Color(255, 255, 255, 1);
                Earth.sprite = HealthyEarth;
            } else if (livesLeft == 2) {
                lifeTwoIcon.GetComponent<Image>().color = new Color(255, 255, 255, 1);
                Earth.sprite = OKEarth;
            }

        }

        playerScore += 50 + ((minutesPassed + 1) * (secondsPassed + (minutesPassed * 60)));

    }

    public void badTargetTapped() {

        //Add 1 to targets destroyed
        //Add 1 to current combo, checking to see if it's the highest
        //Add points

        targetsDestroyed++;
        currentCombo++;
        if (currentCombo > maxCombo) {
            maxCombo++;
        }
        playerScore += 50 + ((minutesPassed + 1) * (secondsPassed + (minutesPassed * 60)));

        if (currentCombo % 10 == 0) {
            alertText.text = currentCombo.ToString("f0") + " target streak!";
            StopCoroutine(alertFade(true));
            StartCoroutine(alertFade(true));
        }

    }

    public void goodTargetTapped() {

        //Add 1 to targets destroyed
        //Combo break
        //Subtract points

        targetsDestroyed++;
        currentCombo = 0;
        playerScore -= (50 + ((minutesPassed + 1) * (secondsPassed + (minutesPassed * 60))))*3;

        //Check if player's score is below 0, then set it to 0 if it is
        if (playerScore < 0) {
            playerScore = 0;
        }

        alertText.text = "Combo breaker!";
        StopCoroutine(alertFade(true));
        StartCoroutine(alertFade(true));

    }

    //Called when a negative target reaches earth
    public void lifeLost() {

        //Subtract life
        //Subtract 5% of player's current score
        //Combo break

        livesLeft -= 1;
        playerScore = Mathf.RoundToInt((float)playerScore * 0.95f);

        currentCombo = 0;

        alertText.text = "Combo breaker!";
        StopCoroutine(alertFade(true));
        StartCoroutine(alertFade(true));

        //Check how many lives the player now has
        if (livesLeft == 2) {
            lifeThreeIcon.GetComponent<Image>().color = new Color(255, 255, 255, 0.5f);
            Earth.sprite = OKEarth;
        } else if (livesLeft == 1) {
            lifeTwoIcon.GetComponent<Image>().color = new Color(255, 255, 255, 0.5f);
            Earth.sprite = UnhealthyEarth;
        } else if (livesLeft == 0) {
            //Begin game over process
            lifeOneIcon.GetComponent<Image>().color = new Color(255, 255, 255, 0.5f);
            print("GAME OVER!");
            CancelInvoke("timerIncrement");
            gameStarted = false;
            StartCoroutine(FadeButton(false));
            pauseButton.interactable = false;

            //Save values for the game over screen
            PlayerPrefs.SetInt("second", secondsPassed);
            PlayerPrefs.SetInt("minute", minutesPassed);
            PlayerPrefs.SetInt("score", playerScore);
            PlayerPrefs.SetInt("combo", maxCombo);
            PlayerPrefs.SetInt("targets", targetsDestroyed);

            StartCoroutine(gameOverFade(true));

        }

    }

    IEnumerator gameOverFade(bool gameOverFade) {

        // increasing i
        for (float i = 0; i <= 1; i += Time.deltaTime) {
            // set color with i as alpha
            FadeView.GetComponent<Image>().color = new Color(0, 0, 0, i);
            yield return null;
        }

        SceneManager.LoadScene("GameOver");

    }

    IEnumerator alertFade(bool alertFade) {

        alertText.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

        //yield return new WaitForSeconds(1);

        // increasing i
        for (float i = 1.0f; i >= 0; i -= Time.deltaTime) {
            // set color with i as alpha
            alertText.color = new Color(1.0f, 1.0f, 1.0f, i);
            yield return null;
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

    IEnumerator FadeButton(bool pauseButtonFadeAway) {

        // fade out
        if (!pauseButtonFadeAway) {
            // decreasing i
            for (float i = 1; i >= 0; i -= Time.deltaTime) {
                // set to white, but with i as alpha
                pauseButton.GetComponent<Image>().color = new Color(255, 255, 255, i);
                yield return null;
            }
        }

        // fade in
        else {
            // increasing i
            for (float i = 0; i <= 1; i += Time.deltaTime) {
                // set to white, but with i as alpha
                pauseButton.GetComponent<Image>().color = new Color(255, 255, 255, i);
                yield return null;
            }
        }
    }

}
