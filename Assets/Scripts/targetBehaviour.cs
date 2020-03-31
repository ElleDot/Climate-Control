using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetBehaviour : MonoBehaviour {

    private Vector3 centrePos;
    public gameScript otherScript;

    void Start() {
        centrePos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 10));
    }

    // Update is called once per frame
    void Update() {

        if (!gameScript.isPaused) {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, centrePos, 1 * Time.deltaTime);
        }

        if (Vector3.Distance(gameObject.transform.position, centrePos) < 3) {
            //Do something
            print("items are too close!");
            Destroy(gameObject);

            GameObject go = GameObject.Find("btn_PauseButton");
            gameScript otherScript = (gameScript)go.GetComponent(typeof(gameScript));

            otherScript.lifeLost();
        }

    }

}
