using SojaExiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorseController : MonoBehaviour {
    private int completedMorse = 0;
    public GameObject door;

    // Update is called once per frame
    void Update() {
        if (completedMorse == 3 && door != null) {
            // Unlock door by setting the boolean value to true
            opencloseDoor doorScript = door.GetComponent<opencloseDoor>();

            if (doorScript != null) {
                doorScript.UnlockDoor();
            }
        }
    }

    public void addCompleted() {
        completedMorse ++;
    }
}
