using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateFlashlight : MonoBehaviour
{

    public Transform Player;
    public GameObject flashLight;

    void OnMouseOver() {
        {
            if (Player) {
                float dist = Vector3.Distance(Player.position, transform.position);
                if (dist < 10) {
                    if (Input.GetMouseButtonDown(0)) {
                        Debug.Log("lol");
                        flashLight.SetActive(!flashLight.activeInHierarchy);
                        this.gameObject.SetActive(false);
                    }

                }
            }
         }


    }
}
