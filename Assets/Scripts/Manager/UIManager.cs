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
    [SerializeField] private GameObject restartCountUI;
    [SerializeField] private GameObject twoBtnAlert;
    [SerializeField] private GameObject oneBtnAlert;

    private void Awake() {
        Singleton();
    }

    public void GameStartBtn() {
        mainUI.SetActive(false);
        inGameUI.SetActive(true);

        GameManager.init.GameStart();
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

    public void ToMainFromInGame() {
        GameManager.init.GameOver();
        GameManager.init.Play();

        restartUI.SetActive(false);
        pauseUI.SetActive(false);
        pauseBtn.SetActive(true);
        inGameUI.SetActive(false);
        mainUI.SetActive(true);
    }

    public void OpenRestartUI() {
        restartUI.SetActive(true);
    }

    public void CloseRestartUI() {
        restartUI.SetActive(false);
    }

    public void ShowAlert(string alertMessage, UnityAction CheckAction) {
        SetAlertMessage(oneBtnAlert, alertMessage);
        oneBtnAlert.GetComponent<AlertInfo>().SetBtnListener(CheckAction);
    }

    public void ShowAlert(string alertMessage, UnityAction CheckAction, UnityAction CancelAction) {
        SetAlertMessage(twoBtnAlert, alertMessage);
        twoBtnAlert.GetComponent<AlertInfo>().SetBtnListener(CheckAction, CancelAction);
    }

    private void SetAlertMessage(GameObject alert, string message) {
        alert.SetActive(true);
        alert.GetComponent<AlertInfo>().SetAlertMessage(message);
    }

    public void RestartCount() {
        restartCountUI.SetActive(true);
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