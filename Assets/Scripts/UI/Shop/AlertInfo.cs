using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Events;

public class AlertInfo : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI alertMessage;
    [SerializeField] private TextMeshProUGUI textCoin;
    [SerializeField] private Button checkBtn;
    [SerializeField] private Button cancelBtn;


    public void SetAlertMessage(string message) {
        alertMessage.text = message;
    }

    public void SetCoinValue(int coin) {
        try {
            textCoin.text = coin.ToString();
        } catch(NullReferenceException NRE) {
            textCoin.text = 0.ToString();
#if (DEBUG)
            UnityEngine.Debug.Log(NRE.Message);
#endif
        }
    }

    public void SetBtnListener(UnityAction CheckAction) {
        ResetCommonListeners();
        if (CheckAction != null)
            checkBtn.onClick.AddListener(CheckAction);
    }

    public void SetBtnListener(UnityAction CheckAction, UnityAction CancelAction) {
        ResetCommonListeners();

        if (CheckAction != null)
            checkBtn.onClick.AddListener(CheckAction);

        if (CancelAction != null) {
            cancelBtn.onClick.AddListener(CancelAction);
        }
    }

    private void ResetCommonListeners() {
        RemoveListeners();
        AddCommonListeners();
    }

    private void RemoveListeners() {
        try {
            checkBtn.onClick.RemoveAllListeners();
            cancelBtn.onClick.RemoveAllListeners();
        }catch (Exception e) {
#if (DEBUG)
            UnityEngine.Debug.Log(e.Message);
#endif
        }
    }

    private void AddCommonListeners() {
        try {
            checkBtn.onClick.AddListener(() => {
                this.gameObject.SetActive(false);
            });
            cancelBtn.onClick.AddListener(() => {
                SoundManager.init.PlaySFXSound(Definition.SoundType.ButtonClose);
                this.gameObject.SetActive(false);
            });
        } catch(Exception e) {
#if (DEBUG)
            UnityEngine.Debug.Log(e.Message);
#endif
        }
    }
}