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

    public int[] arrowOrder = { 0, 1, 3 };

    private int[] currentOrder = { 4, 4, 4 };

    private GameObject[] childrenArray;

    private Renderer[] rendererArray = new Renderer[4];

    private ArrowClicked[] clickedArray = new ArrowClicked[4];

    private int countOrder = 0;

    private AudioSource audioSource;
    public AudioClip wrongAnswerSound;
    public AudioClip correctAnswerSound;

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
            Debug.Log("Child Name: " + child.name);
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
        if( countOrder < 3) {
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

            countOrder++;
        }

        if(countOrder >= 3) {
            correctOrder = true;
            for (int i = 0; i < arrowOrder.Length; i++) {
                if (arrowOrder[i] != currentOrder[i]) {
                    countOrder = 0;
                    correctOrder = false;
                }
            }

            if (correctOrder) {
                doorScript.UnlockDoor();
                audioSource.PlayOneShot(correctAnswerSound);
            }

            else {
                StartCoroutine(WrongAnswer());
            }

            
        }

        
    }

    IEnumerator WrongAnswer() {
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
