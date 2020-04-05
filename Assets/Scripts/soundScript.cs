using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class soundScript : MonoBehaviour {

    private bool isMuted;
    public Image muteSwitch;
    public Sprite onSprite;
    public Sprite offSprite;

    public AudioSource backSound;
    public AudioSource toggleSound;
    public AudioSource selectSound;

    // Start is called before the first frame update
    void Start() {

        isMuted = (PlayerPrefs.GetInt("isMuted") != 0);

        if (isMuted) {
            muteSwitch.sprite = offSprite;
        } else {
            muteSwitch.sprite = onSprite;
        }

    }

    public void muteToggle() {

        isMuted ^= true;

        if (isMuted) {
            PlayerPrefs.SetInt("isMuted", 1);
            muteSwitch.sprite = offSprite;
            print("Game audio DISABLED");
        } else {
            PlayerPrefs.SetInt("isMuted", 0);
            muteSwitch.sprite = onSprite;
            print("Game audio ENABLED");
            toggleSound.Play(0);
        }

    }

    public void backButtonPressed() {

        if (!isMuted) {
            backSound.Play(0);
        }

    }

    public void selectButtonPressed() {
        if (!isMuted) {
            selectSound.Play(0);
        }
    }

    public void clickButtonPressed() {
        if (!isMuted) {
            toggleSound.Play(0);
        }
    }

}
