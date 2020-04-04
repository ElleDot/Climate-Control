using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class aboutScript : MonoBehaviour {

    public GameObject aboutContainer1;
    public GameObject aboutContainer2;
    public GameObject aboutContainer3;

    public Button page1Button;
    public Button page2Button;
    public Button page3Button;

    // Start is called before the first frame update
    void Start() {

        page1Button.interactable = false;

        aboutContainer1.SetActive(true);
        aboutContainer2.SetActive(false);
        aboutContainer3.SetActive(false);

    }

    public void openPage1() {

        page1Button.interactable = false;
        page2Button.interactable = true;
        page3Button.interactable = true;

        aboutContainer1.SetActive(true);
        aboutContainer2.SetActive(false);
        aboutContainer3.SetActive(false);

    }

    public void openPage2() {

        page1Button.interactable = true;
        page2Button.interactable = false;
        page3Button.interactable = true;

        aboutContainer1.SetActive(false);
        aboutContainer2.SetActive(true);
        aboutContainer3.SetActive(false);

    }

    public void openPage3() {

        page1Button.interactable = true;
        page2Button.interactable = true;
        page3Button.interactable = false;

        aboutContainer1.SetActive(false);
        aboutContainer2.SetActive(false);
        aboutContainer3.SetActive(true);

    }

}
