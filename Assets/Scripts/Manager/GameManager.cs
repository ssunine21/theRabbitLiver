using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [HideInInspector]
    public Player player;
    public RecordData recordData;

	private void Awake() {
		Singleton();
	}

    public void InitGame() {
        recordData = new RecordData();
        DataManager.init.InitCurrScore();
        GameStart();
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
        recordData.score = DataManager.init.score.currScore;
        UIManager.init.SetRecordDataUI(recordData.score, recordData.coin, recordData.enemyKill,
            recordData.runCount, recordData.hitCount, recordData.itemCount, recordData.TotalScore());

        Camera.main.GetComponent<CameraControl>().PosReset();
        if (player != null) {
            Destroy(player.gameObject);
        }
        SpawnManager.init.DestroyTileMap();
    } 

    public void FinishGame() {
        GameOver();
        DataManager.init.ChangeScore();
        UIManager.init.GameOverUI();
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