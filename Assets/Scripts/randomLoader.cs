using UnityEngine;
using System.Collections;

public class randomLoader : MonoBehaviour {

    private GameObject[] platforms;
    private float spawnRate;
    private float time;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update() {
        time += Time.deltaTime;
        if (time > spawnRate)
        {
            //Generate Random Number and Instantiate Associated Array Item
            int objectNum = Random.Range(0, 5);
            GameObject platform = Instantiate(platforms[objectNum]);
            //platform.AddComponent<yMovement>();
            //Create Random Start Positon for Object and apply to Object
            Vector3 objectPos = new Vector3((Random.Range(-8, 8)), -3, 0);
            platform.transform.position = objectPos;
            //Reset Time Variable
            time = 0.0f;
        }
    }
}
