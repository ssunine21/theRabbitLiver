using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour {

    [HideInInspector]
    public Player player;
    public RecordData recordData;

    public bool Chance = false;

    public UnityEvent onStartGame;

	private void Awake() {
		Singleton();
        Application.targetFrameRate = 60;
    }

    public void InitGame() {
        recordData = new RecordData();
        DataManager.init.InitCurrScore();
        GameStart();
    }

    public void GameStart() {
        onStartGame.Invoke();
        Chance = true;
        SoundManager.init.ChangeBGM(Definition.SoundType.Stage1);
    }

    public void GameRestart() {
        player.GetComponent<Player>().StartReverse();
    }
    
    public void SetPlayer() {
        player = SpawnManager.init.SpawnPlayer().GetComponent<Player>();
        player.GetComponent<Player>().stamina.SetStamina();
    }

    public void SetObjectOnPlayer() {
        Camera.main.GetComponent<CameraControl>().player = player.gameObject;
        Monster.player = player.gameObject;
    }

    public void GameOver() {
        Camera.main.GetComponent<CameraControl>().PosReset();
        if (player != null) {
            Destroy(player.gameObject);
        }
        SpawnManager.init.DestroyTileMap();
    } 

    public void Goal() {
        StartCoroutine(GoalCheck());
    }

    private void SetRecoreData() {
        recordData.score = DataManager.init.score.currScore;
        UIManager.init.SetRecordDataUI(recordData.score, recordData.coin, recordData.enemyKill,
            recordData.runCount, recordData.hitCount, recordData.itemCount, recordData.TotalScore());
    }

    IEnumerator GoalCheck() {
        player.isGroggy = true;
        yield return new WaitForSeconds(3);
        FinishGame();
    }

    public void FinishGame() {
        SetRecoreData();
        UIManager.init.GameOverUI();
    }

    public void Pause() {
        Debug.Log("pause");
        Time.timeScale = 0;
    }

    public void Play() {
        Debug.Log("Play");
        Time.timeScale = 1;
    }

    private void OnApplicationQuit() {
        //DataManager.init.CloudData.Save();
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