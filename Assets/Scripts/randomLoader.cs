using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class randomLoader : MonoBehaviour {

    private GameObject[] platforms;
    private float spawnRate;
    private float time;
    private bool pauseCheck;
    private bool gameActive;
    private float distanceFromCamera = 10f;
    public gameScript isPaused;
    public gameScript gameStarted;

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
                GameObject platform = Instantiate(platforms[objectNum]);
                //platform.AddComponent<yMovement>();
                //Create Random Start Positon for Object and apply to Object
                Vector3 objectPos = new Vector3((Random.Range(-8, 8)), -3, 10);
                platform.transform.position = objectPos;
                //Reset Time Variable
                time = 0.0f;
            }

        }
        
    }
}
