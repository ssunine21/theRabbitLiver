using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour {

    public DeviceData deviceData;
    public CloudData cloudData;

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
            deviceData = new DeviceData();
            cloudData = new CloudData();
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(this.gameObject);
        }
    }

    public void ChangeCharacterId(int characterId) {
        deviceData.characterId = (DeviceData.CharacterID)characterId;
    }

    public void ChangeScore() {
        if(score.currScore > score.bestScore) {
            score.bestScore = score.currScore;
        }

        score.currScore = 0;
    }
}