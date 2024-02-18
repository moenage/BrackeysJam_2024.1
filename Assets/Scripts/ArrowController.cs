using SojaExiles;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ArrowController : MonoBehaviour
{

    public GameObject door;

    private opencloseDoor doorScript;

    private bool correctOrder = false;

    public int[] arrowOrder = { 0, 1, 3, 2 };

    private int[] currentOrder = { 4, 4, 4, 4 };

    private GameObject[] childrenArray;

    private Renderer[] rendererArray = new Renderer[5];

    private ArrowClicked[] clickedArray = new ArrowClicked[5];

    private int countOrder = 0;

    private AudioSource audioSource;
    public AudioClip wrongAnswerSound;

    void Start() {

        audioSource = GetComponent<AudioSource>();

        doorScript = door.GetComponent<opencloseDoor>();

        if (doorScript == null) {
            Debug.LogError("opencloseDoor script not found on the door GameObject.");
        }

        // Get all children game objects and store them in an array
        childrenArray = GetChildrenArray(transform);

        int count = 0;
        // Now you have an array of all children game objects
        foreach (GameObject child in childrenArray) {

            rendererArray[count] = child.GetComponent<Renderer>();
            clickedArray[count] = child.GetComponent<ArrowClicked>();
            count++;
        }
    }


    GameObject[] GetChildrenArray(Transform parent) {
        // Get all children of the parent
        Transform[] childrenTransforms = parent.GetComponentsInChildren<Transform>();

        int childCount = childrenTransforms.Length - 1;

        // Create an array to store children game objects
        GameObject[] childrenArray = new GameObject[childCount];

        // Fill the array with children game objects
        for (int i = 0; i < childCount; i++) {
            childrenArray[i] = childrenTransforms[i + 1].gameObject;
        }

        return childrenArray;
    }

    public void checkArrows(int arrowNum) {
        if( countOrder < 4) {
            currentOrder[countOrder] = arrowNum;

            if (countOrder == 0) {
                rendererArray[arrowNum].material.color = Color.red;
            }

            else if (countOrder == 1) {
                rendererArray[arrowNum].material.color = Color.green;
            }

            else if (countOrder == 2) {
                rendererArray[arrowNum].material.color = Color.blue;
            }

            else if (countOrder == 3) {
                rendererArray[arrowNum].material.color = Color.black;
            }

            countOrder++;
        }

        if(countOrder >= 4) {
            correctOrder = true;
            for (int i = 0; i < arrowOrder.Length; i++) {
                if (arrowOrder[i] != currentOrder[i]) {
                    countOrder = 0;
                    correctOrder = false;
                }
            }

            if (correctOrder) {
                doorScript.UnlockDoor();
            }

            else {
                StartCoroutine(WrongAnswer());
            }

            
        }

        
    }

    IEnumerator WrongAnswer() {
        countOrder = 0;

        // Play wrong answer sound
        if (audioSource != null ) {
            audioSource.PlayOneShot(wrongAnswerSound);
        }

        // Wait for 1 second
        yield return new WaitForSeconds(1f);

        // Reset colors and unclick status
        for (int k = 0; k < childrenArray.Length; k++) {
            rendererArray[k].material.color = Color.white;
            clickedArray[k].unclicked = true;
        }
    }


}
