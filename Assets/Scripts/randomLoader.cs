using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class randomLoader : MonoBehaviour {

    public static int targetsToCalculate;
    private GameObject[] platforms;
    private float spawnRate;
    private float time;
    private bool pauseCheck;
    private bool gameActive;
    private float distanceFromCamera = 10f;
    private int minutesPassedCheck;
    private int secondsPassedCheck;
    public gameScript isPaused;
    public gameScript gameStarted;
    private Vector3 objectPos;

    // Use this for initialization
    void Start()
    {

        Vector3 centerPos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, distanceFromCamera));

        //Load Platforms into Array
        platforms = Resources.LoadAll<GameObject>("targets");
        // Set Spawn Rate
        spawnRate = 5.0f;

    }

    // Update is called once per frame
    void Update() {

        pauseCheck = GetComponent<gameScript>().isPaused;
        gameActive = GetComponent<gameScript>().gameStarted;

        if (!pauseCheck && !gameStarted) {

            time += Time.deltaTime;

            if (time > spawnRate) {

                print("Tried generating new platform");

                //Generate Random Number and Instantiate Associated Array Item
                int objectNum = Random.Range(0,0);
                GameObject target = Instantiate(platforms[objectNum]);
                //platform.AddComponent<yMovement>();
                //Create Random Start Positon for Object and apply to Object

                //Roll for above or below spawn
                if (Random.Range(0, 2) == 1) {
                    objectPos = new Vector3((Random.Range(-8, 8)), 5, 10);
                } else {
                    objectPos = new Vector3((Random.Range(-8, 8)), -5, 10);
                }
                
                target.transform.position = objectPos;
                //Reset Time Variable
                time = 0.0f;
            }

        }

        //Test to see if the target has been tapped
        if (Input.GetMouseButtonDown(0)) {

            //secondsPassedCheck = gameScript.secondsPassed;
            //minutesPassedCheck = gameScript.minutesPassed;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) {
                //Target has been tapped and can be destroyed
                Destroy(hit.collider.gameObject);

                targetsToCalculate++;

            }

        }

    }

}
