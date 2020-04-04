using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetType { BAD, GOOD }

public class targetBehaviour : MonoBehaviour {

    private Vector3 centrePos;
    public gameScript otherScript;
    [SerializeField]
    public TargetType _targetType;  // I love you Eugene

    void Start() {
        centrePos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 10));
        otherScript = GameObject.FindObjectOfType(typeof(gameScript)) as gameScript;
    }

    // Update is called once per frame
    void Update() {

        if (!gameScript.isPaused) {

            //Test to see if the target is close to the centre
            if (Vector3.Distance(gameObject.transform.position, centrePos) < 3) {

                Destroy(gameObject);

                //Check target type here
                if (_targetType == TargetType.GOOD) {
                    print("good target hit earth!");
                    otherScript.goodObjectHit();
                } else {
                    print("bad target hit earth!");
                    otherScript.lifeLost();
                }
                
            }

            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, centrePos, 1 * Time.deltaTime);

        }

    }

}
