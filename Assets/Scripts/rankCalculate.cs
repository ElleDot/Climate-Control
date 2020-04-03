using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rankCalculate: MonoBehaviour {

    public int secondsPassed;
    public int minutesPassed;
    public string secondString;
    public string minuteString;
    public int gameScore;
    public int targetsDestroyed;
    public int maxCombo;
    public int finalScore;
    public string currentDate;

    public int i;

    public int highScore1;
    public int highScore2;
    public int highScore3;
    public int highScore1Seconds;
    public int highScore1Minutes;
    public int highScore2Seconds;
    public int highScore2Minutes;
    public int highScore3Seconds;
    public int highScore3Minutes;

    public string highScore1Date;
    public string highScore2Date;
    public string highScore3Date;

    public int targetMultiplier;
    public int comboMultiplier;
    public int timerMultiplier;
    public int totalMultiplier;
    public int displayedMultiplier;

    public int displayedScore;
    public float displayedFinalScore;
    public int displayedSeconds;
    public int displayedMinutes;
    public int displayedCombo;
    public int displayedTargets;
    public bool scoreReady;
    public bool timeReady;
    public bool comboReady;
    public bool targetsReady;
    public bool multiReady;

    public Text timeLabel;
    public Text gameScoreLabel;
    public Text targetsDestroyedLabel;
    public Text maxComboLabel;
    public Text finalScoreLabel;
    public Text multipliedScoreLabel;
    public Text durationMultiplierLabel;
    public Text destroyedMultiplierLabel;
    public Text comboMultiplierLabel;
    public Text rankText;

    public Image rankImage;
    public Image PBIcon;

    public Sprite SRank;
    public Sprite ARank;
    public Sprite BRank;
    public Sprite CRank;
    public Sprite DRank;
    public Sprite ERank;
    public Sprite FRank;

    public Button backButton;

    // Start is called before the first frame update
    void Start() {

        backButton.interactable = false;

        rankImage.GetComponent<Image>().color = new Color(255, 255, 255, 0);
        PBIcon.GetComponent<Image>().color = new Color(255, 255, 255, 0);
        rankText.color = new Color(255, 255, 255, 0);

        //Load the variables needed to determine score from the last game
        gameScore = PlayerPrefs.GetInt("score", 0);
        secondsPassed = PlayerPrefs.GetInt("second", 0);
        minutesPassed = PlayerPrefs.GetInt("minute", 0);
        targetsDestroyed = PlayerPrefs.GetInt("targets", 0);
        maxCombo = PlayerPrefs.GetInt("combo", 0);
        
        Invoke("endGameScoreAdd", 1);

        //Test if enough targets were destroyed to raise the multiplier
        if (targetsDestroyed > 200) {
            targetMultiplier += 30;
        } else if (targetsDestroyed >= 100 && targetsDestroyed < 200) {
            targetMultiplier += 20;
        } else if (targetsDestroyed >= 50 && targetsDestroyed < 100) {
            targetMultiplier += 10;
        }

        //Check duration of last game
        if (minutesPassed > 2) {
            timerMultiplier += 30;
        } else if (minutesPassed == 2) {
            timerMultiplier += 20;
        } else if(minutesPassed == 1) {
            timerMultiplier += 10;
        }

        //Test highest combo to see if extra multiplier is granted
        if (maxCombo > 100){
            comboMultiplier += 30;
        } else if (maxCombo >= 50 && maxCombo < 100) {
            comboMultiplier += 20; }
        else if (maxCombo >= 30 && maxCombo < 50) {
            comboMultiplier += 10;
        }

        totalMultiplier = targetMultiplier + timerMultiplier + comboMultiplier;
        displayedMultiplier = totalMultiplier;

        float finalScoreWithMulti = gameScore + (gameScore * (totalMultiplier * 0.01f));
        print(finalScoreWithMulti);

    }

    public void endGameScoreAdd() {
        scoreReady = true;
    }

    public void gameDurationAdd() {
        timeReady = true;
    }

    public void targetsDestroyedAdd() {
        targetsReady = true;
    }

    public void maxComboAdd() {
        comboReady = true;
    }

    public void finalScoreAdd() {
        multipliedScoreLabel.text = gameScore.ToString("n0") + " + " + totalMultiplier.ToString("f0") + "%";
        Invoke("finalScoreWithMulti", 1);
    }

    public void finalScoreWithMulti() {
        multiReady = true;
        displayedFinalScore = gameScore;
    }

    public void rankCheck() {

        rankImage.GetComponent<Image>().color = new Color(255, 255, 255, 1.0f);
        rankText.color = new Color(255, 255, 255, 1.0f);

        if (displayedFinalScore >= 50000) {
            // S RANK
            rankImage.sprite = SRank;
        } else if (displayedFinalScore >= 40000 && displayedFinalScore < 50000) {
            // A RANK
            rankImage.sprite = ARank;
        } else if(displayedFinalScore >= 25000 && displayedFinalScore < 40000) {
            // B RANK
            rankImage.sprite = BRank;
        } else if(displayedFinalScore >= 15000 && displayedFinalScore < 25000) {
            // C RANK
            rankImage.sprite = CRank;
        } else if (displayedFinalScore >= 7500 && displayedFinalScore < 15000) {
            // D RANK
            rankImage.sprite = DRank;
        } else if (displayedFinalScore >= 2500 && displayedFinalScore < 7500) {
            //E RANK
            rankImage.sprite = ERank;
        } else {
            //F RANK
            rankImage.sprite = FRank;
        }

        //Load high scores from the past
        highScore1 = PlayerPrefs.GetInt("hs1", 0);
        highScore1Seconds = PlayerPrefs.GetInt("hs1s", 0);
        highScore1Minutes = PlayerPrefs.GetInt("hs1m", 0);
        highScore1Date = PlayerPrefs.GetString("hs1d", "");
        highScore2 = PlayerPrefs.GetInt("hs2", 0);
        highScore2Seconds = PlayerPrefs.GetInt("hs2s", 0);
        highScore2Minutes = PlayerPrefs.GetInt("hs2m", 0);
        highScore2Date = PlayerPrefs.GetString("hs3d", "");
        highScore3 = PlayerPrefs.GetInt("hs3", 0);
        highScore3Seconds = PlayerPrefs.GetInt("hs3s", 0);
        highScore3Minutes = PlayerPrefs.GetInt("hs3m", 0);
        highScore3Date = PlayerPrefs.GetString("hs3d", "");

        currentDate = System.DateTime.Now.ToString("MM/dd/yyyy");
        
        if (displayedFinalScore > highScore1) {

            print("SAVED SCORE AS NUMBER 1, SHIFTED OLD 1 TO NEW 2 AND OLD 2 TO NEW 3.");

            //High score achieved :)
            PBIcon.GetComponent<Image>().color = new Color(255, 255, 255, 1.0f);
            PlayerPrefs.SetInt("hs1", (int)displayedFinalScore);
            PlayerPrefs.SetInt("hs1s", displayedSeconds);
            PlayerPrefs.SetInt("hs1m", displayedMinutes);
            PlayerPrefs.SetString("hs1d", currentDate);

            //Move old best to 2nd place
            PlayerPrefs.SetInt("hs2", highScore1);
            PlayerPrefs.SetInt("hs2s", highScore1Seconds);
            PlayerPrefs.SetInt("hs2m", highScore1Minutes);
            PlayerPrefs.SetString("hs2d", highScore1Date);

            //Move old 2nd best to 3rd place
            PlayerPrefs.SetInt("hs3", highScore2);
            PlayerPrefs.SetInt("hs3s", highScore2Seconds);
            PlayerPrefs.SetInt("hs3m", highScore2Minutes);
            PlayerPrefs.SetString("hs3d", highScore2Date);

        } else if (displayedFinalScore > highScore2) {

            print("SAVED SCORE AS NUMBER 2, SHIFTED OLD 2 TO NEW 3.");

            //2nd best score achieved
            PlayerPrefs.SetInt("hs2", (int)displayedFinalScore);
            PlayerPrefs.SetInt("hs2s", displayedSeconds);
            PlayerPrefs.SetInt("hs2m", displayedMinutes);
            PlayerPrefs.SetString("hs2d", currentDate);

            //Move old 2nd best to 3rd place
            PlayerPrefs.SetInt("hs3", highScore2);
            PlayerPrefs.SetInt("hs3s", highScore2Seconds);
            PlayerPrefs.SetInt("hs3m", highScore2Minutes);
            PlayerPrefs.SetString("hs3d", highScore2Date);

        } else if (displayedFinalScore > highScore3) {

            print("SAVED SCORE AS NUMBER 3.");

            //3rd best score achieved
            PlayerPrefs.SetInt("hs3", (int)displayedFinalScore);
            PlayerPrefs.SetInt("hs3s", displayedSeconds);
            PlayerPrefs.SetInt("hs3m", displayedMinutes);
            PlayerPrefs.SetString("hs3d", currentDate);

        }

    }

    // Update is called once per frame
    void Update() {

        if (scoreReady) {
            if (displayedScore + 25 < gameScore) {
                displayedScore += 25;
            } else if (displayedScore < gameScore) {
                displayedScore++;
            } else {
                Invoke("gameDurationAdd", 1);
                scoreReady = false;
            }
            gameScoreLabel.text = displayedScore.ToString("n0");
        }

        if (timeReady) {
            if (displayedMinutes < minutesPassed) {
                displayedMinutes++;
            } else {
                if(displayedSeconds < secondsPassed) {
                    displayedSeconds++;
                } else {
                    Invoke("targetsDestroyedAdd", 1);
                    timeReady = false;
                    if (timerMultiplier > 0) {
                        durationMultiplierLabel.text = "+" + timerMultiplier.ToString("f0") + "%";
                    }
                }
            }
            timeLabel.text = displayedMinutes.ToString("f0") + ":" + displayedSeconds.ToString("f0");
        }

        if (targetsReady) {
            if (displayedTargets < targetsDestroyed) {
                displayedTargets++;
            } else {
                Invoke("maxComboAdd", 1);
                targetsReady = false;
                if (targetMultiplier > 0) {
                    destroyedMultiplierLabel.text = "+" + targetMultiplier.ToString("f0") + "%";
                }
                
            }
            targetsDestroyedLabel.text = displayedTargets.ToString("n0");
        }

        if (comboReady) {
            if (displayedCombo < maxCombo) {
                displayedCombo++;
            } else {
                Invoke("finalScoreAdd", 1);
                comboReady = false;
                if (comboMultiplier > 0) {
                    comboMultiplierLabel.text = "+" + comboMultiplier.ToString("f0") + "%";
                }
            }
            maxComboLabel.text = displayedCombo.ToString("n0");
        }

        if (multiReady) {

            i++;
            if (i % 5 == 0) {

                if (displayedMultiplier > 0) {
                    displayedMultiplier -= 1;
                    displayedFinalScore += (gameScore * 0.01f);
                    multipliedScoreLabel.text = displayedFinalScore.ToString("n0") + " + " + displayedMultiplier.ToString("f0") + "%";
                } else {
                    multipliedScoreLabel.text = displayedFinalScore.ToString("n0");
                    multiReady = false;
                    backButton.interactable = true;

                    Invoke("rankCheck", 1);
                }

            }

        }

    }

}