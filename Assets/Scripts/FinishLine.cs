using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] float delay = 1f;
    [SerializeField] ParticleSystem finishEffect;

    bool hasFinished = false;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player" && !hasFinished) {
            hasFinished = true;
            finishEffect.Play();
            GetComponent<AudioSource>().Play();
            FindObjectOfType<PlayerController>().DisableControls();
            Invoke("ReloadScene", delay);
        }
    }

    void ReloadScene() {
        SceneManager.LoadScene(0);
    }
}
