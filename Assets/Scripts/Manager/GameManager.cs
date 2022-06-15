using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [HideInInspector]
    public Player player;

	private void Awake() {
		Singleton();
	}

    public void GameStart() {
        SpawnManager.init.CreateTileMap();
        player = SpawnManager.init.SpawnPlayer().GetComponent<Player>();
        player.GetComponent<Player>().stamina.SetStamina();
        Play();

        Camera.main.GetComponent<CameraControl>().player = player.gameObject;
        Monster.player = player.gameObject;
    }

    public void GameOver() {
        DataManager.init.ChangeScore();
        Camera.main.GetComponent<CameraControl>().PosReset();
        if (player != null) {
            Destroy(player.gameObject);
        }
        SpawnManager.init.DestroyTileMap();
    }

    public void Pause() {
        Time.timeScale = 0;
    }

    public void Play() {
        Time.timeScale = 1;
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