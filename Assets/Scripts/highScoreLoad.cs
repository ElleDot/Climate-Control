using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class highScoreLoad : MonoBehaviour {

    public string hs1mString;
    public string hs1sString;
    public string hs1dString;
    public string hs2mString;
    public string hs2sString;
    public string hs2dString;
    public string hs3mString;
    public string hs3sString;
    public string hs3dString;

    public Text highScore1Label;
    public Text highScore2Label;
    public Text highScore3Label;

    public Image highScore1RankImage;
    public Image highScore2RankImage;
    public Image highScore3RankImage;

    public Sprite SRank;
    public Sprite ARank;
    public Sprite BRank;
    public Sprite CRank;
    public Sprite DRank;
    public Sprite ERank;
    public Sprite FRank;

    // Start is called before the first frame update
    void Start() {

        int highScore1 = PlayerPrefs.GetInt("hs1", 0);
        int highScore1Minutes = PlayerPrefs.GetInt("hs1m", 0);
        int highScore1Seconds = PlayerPrefs.GetInt("hs1s", 0);
        string highScore1Date = PlayerPrefs.GetString("hs1d", "");

        int highScore2 = PlayerPrefs.GetInt("hs2", 0);
        int highScore2Minutes = PlayerPrefs.GetInt("hs2m", 0);
        int highScore2Seconds = PlayerPrefs.GetInt("hs2s", 0);
        string highScore2Date = PlayerPrefs.GetString("hs2d", "");

        int highScore3 = PlayerPrefs.GetInt("hs3", 0);
        int highScore3Minutes = PlayerPrefs.GetInt("hs3m", 0);
        int highScore3Seconds = PlayerPrefs.GetInt("hs3s", 0);
        string highScore3Date = PlayerPrefs.GetString("hs3d", "");

        if (highScore1Minutes < 10) {
            hs1mString = "0" + highScore1Minutes;
        } else {
            hs1mString = highScore1Minutes.ToString("f0");
        }

        if (highScore1Seconds < 10) {
            hs1sString = "0" + highScore1Seconds;
        } else {
            hs1sString = highScore1Seconds.ToString("f0");
        }

        if (highScore2Minutes < 10) {
            hs2mString = "0" + highScore2Minutes;
        } else {
            hs2mString = highScore2Minutes.ToString("f0");
        }

        if (highScore2Seconds < 10) {
            hs2sString = "0" + highScore2Seconds;
        } else {
            hs2sString = highScore2Seconds.ToString("f0");
        }

        if (highScore3Minutes < 10) {
            hs3mString = "0" + highScore3Minutes;
        } else {
            hs3mString = highScore3Minutes.ToString("f0");
        }

        if (highScore3Seconds < 10) {
            hs3sString = "0" + highScore3Seconds;
        } else {
            hs3sString = highScore3Seconds.ToString("f0");
        }

        highScore1Label.text = highScore1.ToString("n0") + " | " + hs1mString + ":" + hs1sString + " | " + highScore1Date;
        highScore2Label.text = highScore2.ToString("n0") + " | " + hs2mString + ":" + hs2sString + " | " + highScore2Date;
        highScore3Label.text = highScore3.ToString("n0") + " | " + hs3mString + ":" + hs3sString + " | " + highScore3Date;

        if (highScore1 == 0) {
            highScore1Label.text = "No score yet!";
            highScore1RankImage.GetComponent<Image>().color = new Color(255, 255, 255, 0);
        }

        if(highScore2 == 0) {
            highScore2Label.text = "No score yet!";
            highScore2RankImage.GetComponent<Image>().color = new Color(255, 255, 255, 0);
        }

        if (highScore3 == 0) {
            highScore3Label.text = "No score yet!";
            highScore3RankImage.GetComponent<Image>().color = new Color(255, 255, 255, 0);
        }

        if (highScore1 >= 50000){
            // S RANK
            highScore1RankImage.sprite = SRank;
        } else if (highScore1 >= 40000 && highScore1 < 50000) {
            // A RANK
            highScore1RankImage.sprite = ARank;
        } else if (highScore1 >= 25000 && highScore1 < 40000) {
            // B RANK
            highScore1RankImage.sprite = BRank;
        } else if (highScore1 >= 15000 && highScore1 < 25000) {
            // C RANK
            highScore1RankImage.sprite = CRank;
        } else if (highScore1 >= 7500 && highScore1 < 15000) {
            // D RANK
            highScore1RankImage.sprite = DRank;
        } else if (highScore1 >= 2500 && highScore1 < 7500) {
            //E RANK
            highScore1RankImage.sprite = ERank;
        } else {
            //F RANK
            highScore1RankImage.sprite = FRank;
        }

        if (highScore2 >= 50000) {
            // S RANK
            highScore2RankImage.sprite = SRank;
        } else if (highScore2 >= 40000 && highScore2 < 50000) {
            // A RANK
            highScore2RankImage.sprite = ARank;
        } else if (highScore2 >= 25000 && highScore2 < 40000) {
            // B RANK
            highScore2RankImage.sprite = BRank;
        } else if (highScore2 >= 15000 && highScore2 < 25000) {
            // C RANK
            highScore2RankImage.sprite = CRank;
        } else if (highScore2 >= 7500 && highScore2 < 15000) {
            // D RANK
            highScore2RankImage.sprite = DRank;
        } else if (highScore2 >= 2500 && highScore2 < 7500) {
            //E RANK
            highScore2RankImage.sprite = ERank;
        } else {
            //F RANK
            highScore2RankImage.sprite = FRank;
        }

        if (highScore3 >= 50000) {
            // S RANK
            highScore3RankImage.sprite = SRank;
        } else if (highScore3 >= 40000 && highScore3 < 50000) {
            // A RANK
            highScore3RankImage.sprite = ARank;
        } else if (highScore3 >= 25000 && highScore3 < 40000) {
            // B RANK
            highScore3RankImage.sprite = BRank;
        } else if (highScore3 >= 15000 && highScore3 < 25000) {
            // C RANK
            highScore3RankImage.sprite = CRank;
        } else if (highScore3 >= 7500 && highScore3 < 15000) {
            // D RANK
            highScore3RankImage.sprite = DRank;
        } else if (highScore3 >= 2500 && highScore3 < 7500) {
            //E RANK
            highScore3RankImage.sprite = ERank;
        } else {
            //F RANK
            highScore3RankImage.sprite = FRank;
        }

    }

}
