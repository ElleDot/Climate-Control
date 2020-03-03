using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class viewLoad : MonoBehaviour {

    public string Destination;
    private bool fadeAway;
    public Image FadeView;
    public GameObject FadeViewObj;
    

    // Start is called before the first frame update
    void Start() {

        // fades the image out when view loaded
        StartCoroutine(FadeImage(false));
        fadeAway = false;

    }

    // Define new functions here
    public void LoadView() {

        FadeViewObj.SetActive(true);
        StartCoroutine(FadeImage(true));
        fadeAway = true;

    }

    public void enableDisable() {

        if (fadeAway) {
            SceneManager.LoadScene(Destination);
        } else {
            FadeViewObj.SetActive(false);
        }

    }

    IEnumerator FadeImage(bool fadeAway) {

        Invoke("enableDisable", 1);

        // fade out
        if (!fadeAway) {
            // decreasing i
            for (float i = 1; i >= 0; i -= Time.deltaTime) {
                // set color with i as alpha
                FadeView.GetComponent<Image>().color = new Color(0, 0, 0, i);
                yield return null;
            }
        }

        // fade in
        else {
            // increasing i
            for (float i = 0; i <= 1; i += Time.deltaTime) {
                // set color with i as alpha
                FadeView.GetComponent<Image>().color = new Color(0, 0, 0, i);
                yield return null;
            }
        }
    }

}
