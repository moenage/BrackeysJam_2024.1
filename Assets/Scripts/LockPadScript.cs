using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPadScript : MonoBehaviour
{

    public static event Action<string, int> Rotated = delegate { };
    public Transform Player;

    private bool coroutineAllowed;

    private int currentNum;

    private void Start() { 
        coroutineAllowed = true;
        currentNum = 1;
    }

    private void OnMouseDown() {
        if (Player) {
            float dist = Vector3.Distance(Player.position, transform.position);
            if (dist < 5) {
                if (coroutineAllowed) {
                    StartCoroutine("RotatePad");
                }
            }
        }
    }


    private IEnumerator RotatePad() {
        coroutineAllowed = false;


        // This rotates it in increments of 8. So 8 x 5 is 45 degrees on every click
        // It's just spread out over multiple milliseconds for better animation
        for(int i = 0; i <= 8; i++) {
            transform.Rotate(0f, 0f, -5f);
            yield return new WaitForSeconds(0.01f);
        }

        coroutineAllowed = true;

        currentNum++;

        if(currentNum > 8) {
            currentNum = 1;
        }

        Rotated(name, currentNum);

    }


}
