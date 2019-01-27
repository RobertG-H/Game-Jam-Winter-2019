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

    public GameObject seapleWin;
    public GameObject sharkWin;
    public GameObject pressKey;

    bool gameOver = false;

    // Use this for initialization
    void Start () {
        sharkWin.SetActive(false);
        seapleWin.SetActive(false);
        pressKey.SetActive(false);
        gameOver = false;
        
        isPaused = false;	
	}

	void Update () {
		if (Input.GetKeyDown("p") || Input.GetKeyDown("escape")) {
            TogglePause();
        }
        else if (Input.GetKeyDown("r")) {
            Restart();
        }

        if (gameOver && Input.anyKey) {
            Time.timeScale = 1;
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
        if (playersAlive == 0) {
            gameOver = true;
            sharkWin.SetActive(true);
            pressKey.SetActive(true);
            //END GAME
            sfxSource.clip = winning;
            sfxSource.loop = false;
            sfxSource.Play ();
            Time.timeScale = 0;
        }
    }

    public void playerRevived() {
        playersAlive++;
    }

    public void playerWin() {
        if (playersAlive > 1) {
            gameOver = true;
            seapleWin.SetActive(true);
            pressKey.SetActive(true);
            sfxSource.clip = winningPlayers;
            sfxSource.loop = false;
            sfxSource.Play ();

            Time.timeScale = 0;
            playersAlive = -5;
        }
    }
}
