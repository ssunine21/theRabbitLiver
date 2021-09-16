using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour {

    public UserData userData;

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
            userData = new UserData();
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(this.gameObject);
        }
    }

    public void ChangeCharacterId(int characterId) {
        userData.characterId = (UserData.CharacterID)characterId;
    }

    public void ChangeScore() {
        if(score.currScore > score.bestScore) {
            score.bestScore = score.currScore;
        }
    }
}