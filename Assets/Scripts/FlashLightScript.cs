using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightScript : MonoBehaviour
{

    public GameObject flashLight;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) {
            flashLight.SetActive(!flashLight.activeInHierarchy);
        }
    }
}
