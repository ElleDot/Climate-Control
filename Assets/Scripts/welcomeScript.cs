using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class welcomeScript : MonoBehaviour {

    public GameObject welcomeView;
    public GameObject welcomeBox;
    public GameObject welcomeButton;
    public Text welcomeTitle;
    public Text welcomeBody;
    private int isFirstLoad = 0;

    // Start is called before the first frame update
    void Start() {

        isFirstLoad = PlayerPrefs.GetInt("isFirstLoad", 0);

        if (isFirstLoad == 1) {
            welcomeView.SetActive(false);
        }

        PlayerPrefs.SetInt("isFirstLoad", 1);

    }

    public void closeWindow() {

        StartCoroutine(FadeWelcome(true));

    }

    IEnumerator FadeWelcome(bool willDisappear) {

        // fadeout for welcome box
        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            // set to white, but with i as alpha
            welcomeView.GetComponent<Image>().color = new Color(0, 0, 0, i);
            welcomeBox.GetComponent<Image>().color = new Color(255, 255, 255, i);
            welcomeButton.GetComponent<Image>().color = new Color(255, 255, 255, i);
            welcomeTitle.color = new Color(0.2f, 0.2f, 0.2f, i);
            welcomeBody.color = new Color(0.2f, 0.2f, 0.2f, i);
            yield return null;
        }

        welcomeView.SetActive(false);

    }

}
