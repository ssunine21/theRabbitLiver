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
        SpawnManager.init.CreateTileMap();
        player = SpawnManager.init.SpawnPlayer();

        ResetGameSettings();

        Camera.main.GetComponent<CameraControl>().player = player;
        Monster.player = player;
    }

    public void GameOver() {
        DataManager.init.ChangeScore();
        Camera.main.GetComponent<CameraControl>().PosReset();
        if (player != null) {
            Destroy(player);
        }

        SpawnManager.init.DestroyTileMap();
    }

    public void GoHome() {
        Play();
    }

    public void Pause() {
        Time.timeScale = 0;
    }

    public void Play() {
        Time.timeScale = 1;
    }

    private void ResetGameSettings() {
        Play();
        player.GetComponent<Player>().stamina.SetStamina();
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