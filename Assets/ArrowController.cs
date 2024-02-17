using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    void Start() {
        // Get all children game objects and store them in an array
        GameObject[] childrenArray = GetChildrenArray(transform);

        // Now you have an array of all children game objects
        foreach (GameObject child in childrenArray) {
            Debug.Log("Child Name: " + child.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
