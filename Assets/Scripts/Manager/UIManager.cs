using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour {

    [SerializeField] private GameObject mainUI;
    [SerializeField] private GameObject shopUI;
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject pauseBtn;
    [SerializeField] private GameObject restartUI;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject restartCountUI;
    [SerializeField] private GameObject twoBtnAlert;
    [SerializeField] private GameObject twoBtnAlertWithCoin;
    [SerializeField] private GameObject oneBtnAlert;
    [SerializeField] private GameObject loadingPanel;


    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI coin;
    [SerializeField] private TextMeshProUGUI enemyKill;
    [SerializeField] private TextMeshProUGUI runCount;
    [SerializeField] private TextMeshProUGUI hitCount;
    [SerializeField] private TextMeshProUGUI itemCount;
    [SerializeField] private TextMeshProUGUI totalScore;

    public GameObject Title { get => _title; }
    [SerializeField] private GameObject _title;

    public GameObject NoTouchPanel { get => _noTouchPanel;}
    [SerializeField] private GameObject _noTouchPanel;

    [SerializeField] private Curtain _curtain;

    private void Awake() {
        Singleton();
    }

    private void Start() {
        StartCoroutine(CoInitGame());
    }

    public bool isInitGame = false;
    IEnumerator CoInitGame() {
        float timer = 5f;
        ShowLoading(true);

        while (timer > 0) {
            if (isInitGame) {
                StartCoroutine(Camera.main.GetComponent<CameraControl>().CameraMoveAtFirstStart());
                _curtain.StartFadeIn();
                NoTouchPanel.SetActive(true);

                ShowLoading(false);
                yield break;
            }

            timer -= Time.deltaTime;
            yield return null;
        }

        StartCoroutine(Camera.main.GetComponent<CameraControl>().CameraMoveAtFirstStart());
        _curtain.StartFadeIn();
        NoTouchPanel.SetActive(true);
        ShowLoading(false);
    }

    public void GameStartBtn() {
        //StartCoroutine(CoFadeInOut(() => {
            mainUI.SetActive(false);
            inGameUI.SetActive(true);
            GameManager.init.InitGame();
        //}));
    }

    public void ShopBtn() {
        shopUI.SetActive(true);
        mainUI.SetActive(false);
    }

    public void ToMainFromShopBtn() {
        shopUI.SetActive(false);
        mainUI.SetActive(true);
    }

    public void PauseBtn() {
        pauseBtn.SetActive(false);
        pauseUI.SetActive(true);

        GameManager.init.Pause();
    }

    public void PlayBtn() {
        pauseBtn.SetActive(true);
        pauseUI.SetActive(false);

        GameManager.init.Play();
    }

    public void RestartBtn() {
        ShowAlert(Definition.RESTART_MESSAGE, GameRestart, null);
    }

    private void GameRestart() {
        GameManager.init.Play();

        _curtain.FadeInOut(() => {
            GameManager.init.GameOver();
            GameManager.init.InitGame();
            GameManager.init.Play();

            GameManager.init.player.isDead = true;
            restartUI.SetActive(false);
            pauseUI.SetActive(false);
            pauseBtn.SetActive(true);

            restartCountUI.GetComponent<RestartCount>().Count(() => {
                GameManager.init.player.isDead = false;
            });
        });
    }

    public void GameOverUI() {
        restartUI.SetActive(false);
        gameOverUI.SetActive(true);
    }

    public void ToMainFromInGame() {
        GameManager.init.Play();
        _curtain.FadeInOut(() => {
            GameManager.init.GameOver();
            GameManager.init.Play();

            restartUI.SetActive(false);
            gameOverUI.SetActive(false);
            pauseUI.SetActive(false);
            pauseBtn.SetActive(true);
            inGameUI.SetActive(false);
            mainUI.SetActive(true);

            AdsManager.init.ShowInterstitialAd();
        });
    }

    public void OpenRestartUI() {
        restartUI.SetActive(true);
    }

    public void CloseRestartUI() {
        restartUI.SetActive(false);
    }

    public void ShowLoading(bool isShow) {
        if (loadingPanel.activeSelf == isShow) return;
        loadingPanel.SetActive(isShow);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="alertMessage"></param>
    /// <param name="CheckAction"></param>
    public void ShowAlert(string alertMessage, UnityAction CheckAction) {
        SetAlertMessage(oneBtnAlert, alertMessage);
        oneBtnAlert.GetComponent<AlertInfo>().SetBtnListener(CheckAction);
    }

    public void ShowAlert(string alertMessage, UnityAction CheckAction, UnityAction CancelAction) {
        SetAlertMessage(twoBtnAlert, alertMessage);
        twoBtnAlert.GetComponent<AlertInfo>().SetBtnListener(CheckAction, CancelAction);
    }

    public void ShowAlert(string alertMessage, int coin, UnityAction CheckAction, UnityAction CancelAction) {
        SetAlertMessage(twoBtnAlertWithCoin, alertMessage, coin);
        twoBtnAlertWithCoin.GetComponent<AlertInfo>().SetBtnListener(CheckAction, CancelAction);
    }

    private void SetAlertMessage(GameObject alert, string message) {
        alert.SetActive(true);
        alert.GetComponent<AlertInfo>().SetAlertMessage(message);
    }

    private void SetAlertMessage(GameObject alert, string message, int coin) {
        alert.SetActive(true);
        alert.GetComponent<AlertInfo>().SetAlertMessage(message);
        alert.GetComponent<AlertInfo>().SetCoinValue(coin);
    }

    public void RestartCount() {
        restartCountUI.GetComponent<RestartCount>().Count(() => {
            GameManager.init.player.GetComponent<Player>().reverseWhenEndDeathAnimation();
            this.gameObject.SetActive(false);
        });
    }

    public void SetRecordDataUI(int score, int coin, int enemyKill, int runCount,
        int hitCount, int itemCount, int totalScore) {
        this.score.text = score.ToString();
        this.coin.text = coin.ToString();
        this.enemyKill.text = enemyKill.ToString();
        this.runCount.text = runCount.ToString();
        this.hitCount.text = hitCount.ToString();
        this.itemCount.text = itemCount.ToString();
        this.totalScore.text = totalScore.ToString();
        
        DataManager.init.ChangeScore(totalScore);
    }

    public static UIManager init;
    private void Singleton() {
        if (init == null) {
            init = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(this.gameObject);
        }
    }
}