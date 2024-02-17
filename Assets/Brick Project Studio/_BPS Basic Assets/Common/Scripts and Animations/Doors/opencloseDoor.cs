using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SojaExiles

{
	public class opencloseDoor : MonoBehaviour
	{

		public Animator openandclose;
		public bool open = false;
        public bool unlocked = false;
		public Transform Player;

		void OnMouseOver()
		{
			{
				if (unlocked) {
					if (Player) {
						float dist = Vector3.Distance(Player.position, transform.position);
						if (dist < 10) {
							if (open == false) {
								if (Input.GetMouseButtonDown(0)) {
									StartCoroutine(opening());
								}
							}
							else {
								if (open == true) {
									if (Input.GetMouseButtonDown(0)) {
										StartCoroutine(closing());
									}
								}

							}

						}
					}
				}

			}

		}

		IEnumerator opening()
		{
			print("you are opening the door");
			openandclose.Play("Opening");
			open = true;
			yield return new WaitForSeconds(.5f);
		}

		IEnumerator closing()
		{
			print("you are closing the door");
			openandclose.Play("Closing");
			open = false;
			yield return new WaitForSeconds(.5f);
		}

        public void UnlockDoor() {
			unlocked = true;
		}


    }
}