using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;

public class DataManager : MonoBehaviour {

    public DeviceData DeviceData;
    public CloudData CloudData;

    private DatabaseReference databaseReference;

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
        CloudData.Load();
    }

    private void Start() {
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void ChangeCharacterId(int characterId) {
        DeviceData.characterId = (DeviceData.CharacterID)characterId;
    }

    public void InitCurrScore() {
        score.currScore = 0;
    }

    public void ChangeScore() {
        if(score.currScore > score.bestScore) {
            score.bestScore = score.currScore;
        }
        InitCurrScore();
    }

    public void LoadToFirebase(string ID) {
    }

    /// <summary>
    /// Comparison Coin.
    /// </summary>
    /// <param name="price">Item price</param>
    /// <param name="isBuy">If you want pay when enoght coin, this value is true</param>
    /// <returns></returns>
    public bool CoinComparison(int price, bool isBuy = false) {
        if (CloudData.coin >= price) {
            if (isBuy) CoinPayment(price);
            return true;
        } else {
            return false;
        }
    }

    public void CoinPayment(int price) {
        CloudData.coin -= price;
    }


    public static DataManager init;
    private void Singleton() {
        if (init == null) {
            init = this;
            DeviceData = new DeviceData();
            CloudData = new CloudData();
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(this.gameObject);
        }
    }
}