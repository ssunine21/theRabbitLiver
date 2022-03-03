using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Events;

public class AlertInfo : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI alertMessage;
    [SerializeField] private Button checkBtn;
    [SerializeField] private Button cancelBtn;


    public void SetAlertMessage(string message) {
        alertMessage.text = message;
    }

    public void SetBtnListener(UnityAction CheckAction) {
        ResetCommonListeners();
        checkBtn.onClick.AddListener(CheckAction);
    }

    public void SetBtnListener(UnityAction CheckAction, UnityAction CancelAction) {
        ResetCommonListeners();
        checkBtn.onClick.AddListener(CheckAction);
        cancelBtn.onClick.AddListener(CancelAction);
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

        }
    }

    private void AddCommonListeners() {
        try {
            checkBtn.onClick.AddListener(() => {
                this.gameObject.SetActive(false);
            });
            cancelBtn.onClick.AddListener(() => {
                this.gameObject.SetActive(false);
            });
        } catch(Exception e) {

        }
    }
}