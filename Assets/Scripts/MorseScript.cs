using SojaExiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class MorseScript : MonoBehaviour {
    public Transform Player;
    private float clickDuration = 0.3f; // Adjust as needed
    private string currentInput = "";
    private float currentInputTimer = 0f;
    private bool translationInProgress = false;
    public bool completed = false;
    private string completedInput = "";
    public string answer;

    private GameObject morseController;

    private AudioSource audioSource;
    private Renderer objectRenderer;

    public float timeToCheckAnswer = 0.5f; // Adjust as needed
    private float lastInputTime = 0f;

    private Coroutine flashCoroutine; // Reference to the flashing coroutine

    void Start() {
        audioSource = GetComponent<AudioSource>();
        objectRenderer = GetComponent<Renderer>();
        morseController = this.transform.parent.gameObject;
    }

    void OnMouseOver() {
        if (completed) {
            // Do nothing
        }
        else if (Player) {
            float dist = Vector3.Distance(Player.position, transform.position);
            if (dist < 10) {
                if (Input.GetMouseButtonDown(0) && !translationInProgress) {
                    // Mouse button clicked
                    currentInputTimer = Time.time;
                    currentInput = ".";
                    translationInProgress = true;
                    audioSource.Play(); // Play the held sound when translation starts
                }

                if (Input.GetMouseButton(0)) {
                    // Mouse button held
                    float heldTime = Time.time - currentInputTimer;

                    if (heldTime >= clickDuration) {
                        // Click and hold for a certain duration, consider it a Morse code input
                        currentInput = "-";
                        currentInputTimer = Time.time;
                        translationInProgress = true;
                    }
                }

                if (Input.GetMouseButtonUp(0) && translationInProgress) {
                    // Translate Morse code and reset input
                    translationInProgress = false;
                    audioSource.Stop(); // Stop the held sound when translation ends
                    lastInputTime = Time.time; // Update the last input time
                    completedInput += currentInput;
                }
            }
        }
    }

    void Update() {
        if (!completed && completedInput != "" && Time.time - lastInputTime > timeToCheckAnswer) {
            checkAnswer();
        }
    }

    void checkAnswer() {
        if (completedInput == answer) {
            objectRenderer.material.color = Color.green;
            completed = true;

            // Unlock door by setting the boolean value to true
            MorseController morseContScript = morseController.GetComponent<MorseController>();

            morseContScript.addCompleted();
        }
        else {
            completedInput = "";
            if (flashCoroutine == null) {
                // Start flashing coroutine only if it's not running
                flashCoroutine = StartCoroutine(FlashRed());
            }
        }
    }

    IEnumerator FlashRed() {
        float flashDuration = 0.5f;
        float elapsedTime = 0f;

        while (elapsedTime < flashDuration) {
            objectRenderer.material.color = Color.Lerp(Color.white, Color.red, Mathf.PingPong(elapsedTime, 1f));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0f;

        while (elapsedTime < flashDuration) {
            objectRenderer.material.color = Color.Lerp(Color.red, Color.white, Mathf.PingPong(elapsedTime, 1f));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Set the coroutine reference to null to indicate it's not running anymore
        flashCoroutine = null;
    }
}
