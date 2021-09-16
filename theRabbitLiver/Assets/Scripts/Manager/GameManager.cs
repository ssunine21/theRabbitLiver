using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private GameObject player;

	private void Awake() {
		Singleton();
	}

    private void Start() {
        
    }

    public void GameStart() {
        SpawnManager.init.TileUpDownAnimStart();
        player = SpawnManager.init.SpawnPlayer();

        ResetGameSettings();

        Camera.main.GetComponent<CameraControl>().player = player;
    }

    public void GameOver() {
        DataManager.init.ChangeScore();
        if (player != null) {
            Destroy(player);
        }
    }

    public void GoHome() {
        
    }

    public void Pause() {
        Time.timeScale = 0;
    }

    public void Play() {
        Time.timeScale = 1;
    }

    private void ResetGameSettings() {
        player.GetComponent<Player>().stamina.SetStamina();
        DataManager.init.score.currScore = 0;
    }

    public static GameManager init;
	private void Singleton() {
        if (init == null) {
            init = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else {
            Destroy(this.gameObject);
        }
    }
}