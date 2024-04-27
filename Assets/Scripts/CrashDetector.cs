using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    [SerializeField] float delay = 0.5f;
    [SerializeField] ParticleSystem crashEffect;
    [SerializeField] AudioClip crashSFX;

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log(this.gameObject.tag);
        if (other.tag == "Ground" && this.gameObject.tag != "Boarder Bottom") {
            Debug.Log(this.gameObject.tag);
            FindObjectOfType<PlayerController>().DisableControls();
            crashEffect.Play();
            GetComponent<AudioSource>().PlayOneShot(crashSFX);
            Debug.Log("Crashed!");
            Invoke("ReloadScene", delay);
        }
    }

    void ReloadScene() {
        SceneManager.LoadScene(0);
    }
}
