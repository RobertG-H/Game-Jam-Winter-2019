using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    [SerializeField]
    string MainMenuScene;

    bool isPaused;

    int playersAlive = 3;

    public GameObject PauseUI;

    public AudioSource sfxSource;
    public AudioClip winning;
    public AudioClip winningPlayers;

    // Use this for initialization
    void Start () {
        isPaused = false;	
	}

	void Update () {
		if (Input.GetKeyDown("p") || Input.GetKeyDown("escape")) {
            TogglePause();
        }
        else if (Input.GetKeyDown("r")) {
            Restart();
        }
	}

    public void TogglePause() {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        PauseUI.SetActive(isPaused);
    }

    public void Restart() {
        if (isPaused) {
            TogglePause();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(MainMenuScene);
    }

    public void playerDied() {
        playersAlive--;
        Debug.Log ("PLAYER DIED " + playersAlive);
        if (playersAlive == 0) {
            //END GAME
            sfxSource.clip = winning;
            sfxSource.loop = false;
            sfxSource.Play ();

            Debug.Log ("SHARK WINS");
            Time.timeScale = 0;
        }
    }

    public void playerRevived() {
        playersAlive++;
    }

    public void playerWin() {
        if (playersAlive > 1) {
            //END GAME
            sfxSource.clip = winningPlayers;
            sfxSource.loop = false;
            sfxSource.Play ();

            Debug.Log ("Seaples WIN");
            Time.timeScale = 0;
            playersAlive = -5;
        }
    }
}
