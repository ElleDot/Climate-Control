using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class welcomeScript : MonoBehaviour {

    public GameObject welcomeView;
    public GameObject welcomeBox;
    public GameObject welcomeButton;
    public GameObject confirmButtonObject;
    public Button confirmButton;
    public Text welcomeTitle;
    public Text welcomeBody;
    private int isFirstLoad = 0;

    // Start is called before the first frame update
    void Start() {

        //isFirstLoad = PlayerPrefs.GetInt("isFirstLoad", 0);

        if (isFirstLoad == 0) {
            welcomeView.SetActive(true);
        }

        PlayerPrefs.SetInt("isFirstLoad", 1);

    }

    public void closeWindow() {

        StartCoroutine(FadeWelcome(true));

    }

    public void openAlbertURL() {

        welcomeView.SetActive(true);
        StartCoroutine(FadeWelcome2(true));

        confirmButtonObject.SetActive(true);

        welcomeBody.alignment = TextAnchor.UpperCenter;
        welcomeTitle.text = "Are you sure?";
        welcomeBody.text = "This will take you to the We Are Albert Homepage.\nPlease note Climate Control is in no way affiliated with We Are Albert.\n(URL: https://wearealbert.org/)";
        welcomeTitle.fontSize = 50;
        welcomeBody.fontSize = 30;
    }

    public void confirmButtonPressed() {

        Application.OpenURL("https://wearealbert.org//");
        Debug.Log("Website load attempted");

    }

    IEnumerator FadeWelcome(bool welcomeFadeInOut) {

        confirmButtonObject.SetActive(false);

        // fadeout for welcome box
        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {

            welcomeView.GetComponent<Image>().color = new Color(0, 0, 0, i);
            welcomeBox.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, i);
            welcomeButton.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, i);
            confirmButton.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, i);
            welcomeTitle.color = new Color(0.2f, 0.2f, 0.2f, i);
            welcomeBody.color = new Color(0.2f, 0.2f, 0.2f, i);
            yield return null;
        }

        welcomeView.SetActive(false);

    }

    IEnumerator FadeWelcome2(bool welcomeFadeInOut) {

        welcomeView.SetActive(true);

        // fadeout for welcome box
        for (float i = 0.0f; i <= 1.0f; i += Time.deltaTime) {

            welcomeView.GetComponent<Image>().color = new Color(0, 0, 0, i);
            welcomeBox.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, i);
            welcomeButton.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, i);
            confirmButton.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, i);
            welcomeTitle.color = new Color(0.2f, 0.2f, 0.2f, i);
            welcomeBody.color = new Color(0.2f, 0.2f, 0.2f, i);
            yield return null;
        }

    }

}
