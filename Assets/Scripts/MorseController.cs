using SojaExiles;
using UnityEngine;

public class MorseController : MonoBehaviour {
    private int completedMorse = 0;
    public GameObject door;

    private opencloseDoor doorScript;

    private AudioSource audioSource;
    public AudioClip correctAnswerSound;

    // Start is called before the first frame update
    void Start() {
        audioSource = GetComponent<AudioSource>();

        // Try to get the opencloseDoor script when the MorseController is initialized
        doorScript = door.GetComponent<opencloseDoor>();

        if (doorScript == null) {
            Debug.LogError("opencloseDoor script not found on the door GameObject.");
        }
    }

    // Update is called once per frame
    void Update() {
        if (completedMorse == 3 && doorScript != null) {
            // Unlock door by setting the boolean value to true
            doorScript.UnlockDoor();
            audioSource.PlayOneShot(correctAnswerSound);
        }
    }

    public void addCompleted() {
        completedMorse++;
    }
}
