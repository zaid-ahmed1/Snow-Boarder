using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FinishLine : MonoBehaviour
{
    [SerializeField] float delay = 1f;
    [SerializeField] ParticleSystem finishEffect;

    GameObject leaderBoard;

    private void Start() {
        leaderBoard = GameObject.Find("Canvas (1)");
        leaderBoard.SetActive(false);
    }
    

    bool hasFinished = false;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player" && !hasFinished) {
            hasFinished = true;
            finishEffect.Play();
            GetComponent<AudioSource>().Play();
            FindObjectOfType<PlayerController>().DisableControls();
            Invoke("showLeaderboard", delay);
        }
    }


    public void ReloadScene() {
        SceneManager.LoadScene(0);
    }

    void showLeaderboard() {
        leaderBoard.SetActive(true);
    }
}
