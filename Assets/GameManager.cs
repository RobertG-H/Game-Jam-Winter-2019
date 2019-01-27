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
        //PauseUI.SetActive(isPaused);
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
            Debug.Log ("SHARK WINS");
            Time.timeScale = 0;
        }
    }

    public void playerRevived() {
        playersAlive++;
    }
}
