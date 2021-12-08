using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour {

    public DeviceData DeviceData {
        get; set;
    }
    public CloudData CloudData {
        get; set;
    }

    private Score _score;
    public Score score {
        get {
            if(_score == null) {
                _score = FindObjectOfType(typeof(Score)) as Score;
            }
            return _score;
        }
    }

    private void Awake() {
        Singleton();
    }

    public static DataManager init;
    private void Singleton() {
        if(init == null) {
            init = this;
            DeviceData = new DeviceData();
            CloudData = new CloudData();
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(this.gameObject);
        }
    }

    private void Start() {
        CloudData.Load();
    }

    public void ChangeCharacterId(int characterId) {
        DeviceData.characterId = (DeviceData.CharacterID)characterId;
    }

    public void ChangeScore() {
        if(score.currScore > score.bestScore) {
            score.bestScore = score.currScore;
        }

        score.currScore = 0;
    }
}