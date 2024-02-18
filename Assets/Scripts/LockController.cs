using SojaExiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LockController : MonoBehaviour
{
    public GameObject door;

    private opencloseDoor doorScript;



    public int[] correctCombo = { 6, 1, 7 };
    private int[] result = { 1, 1, 1 }; // Starting combination
    // Start is called before the first frame update
    void Start()
    {
        doorScript = door.GetComponent<opencloseDoor>();

        if (doorScript == null) {
            Debug.LogError("opencloseDoor script not found on the door GameObject.");
        }

        LockPadScript.Rotated += CheckPad;
    }

    private void CheckPad(string padName, int currentNum) {

        switch(padName) {
            case "LockPad":
                result[0] = currentNum;
                break;

            case "LockPad (1)":
                result[1] = currentNum;
                break;

            case "LockPad (2)":
                result[2] = currentNum;
                break;
        }

        //Debug.Log(result[0] + ", " + result[1] + ", " + result[2]);

        if (result[0] == correctCombo[0] && result[1] == correctCombo[1] && result[2] == correctCombo[2]) {

            //Debug.Log("DONEZOOOOOOOOOO");
            doorScript.UnlockDoor();
            LockPadScript.Rotated -= CheckPad;

        }
    }

    private void OnDestroy() {
        LockPadScript.Rotated -= CheckPad;
    }

}
