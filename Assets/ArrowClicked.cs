using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ArrowClicked : MonoBehaviour
{

    public bool unclicked = true;
    public Transform Player;

    private void OnMouseDown() {
        if (unclicked) {
            if (Player) {
                float dist = Vector3.Distance(Player.position, transform.position);
                if (dist < 10) {
                    
                    unclicked = false;

                }
            }
        }
    }

}
