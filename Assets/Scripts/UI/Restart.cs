using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Restart : MonoBehaviour {
    private readonly float RESTART_UI_TIME = 5f;
    private readonly int RESTART_COIN = 1500;

    [SerializeField] private Image reImg;
    [SerializeField] private Button coinBtn;
    [SerializeField] private Button adsBtn;

    private void OnEnable() {
        StartCoroutine(nameof(UITime));
        CompareCoin();
    }

    IEnumerator UITime() {
        float currTime = RESTART_UI_TIME;

        while(currTime >= 0) {
            currTime -= Time.deltaTime;
            reImg.fillAmount = (currTime / RESTART_UI_TIME);
            yield return null;
        }
        GameManager.init.FinishGame();
    }

    private void CompareCoin() {
        coinBtn.interactable = DataManager.init.CloudData.coin >= RESTART_COIN;
    }

    public void CoinBtn() {
        DataManager.init.CloudData.coin -= RESTART_COIN;
        GameManager.init.GameRestart();
    }
}
