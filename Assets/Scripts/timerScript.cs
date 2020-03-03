using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timerScript : MonoBehaviour {

    public int minutesPassed;
    public int secondsPassed;
    private string minuteString;
    private string secondString;



    // Start is called before the first frame update
    void Start() {

        minutesPassed = 0;
        secondsPassed = 0;

        Invoke("timerIncrement", 3);

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

            GetComponent<Text>().text = minuteString + ":" + secondString;

    }

    public void playPause() {



    }

}
