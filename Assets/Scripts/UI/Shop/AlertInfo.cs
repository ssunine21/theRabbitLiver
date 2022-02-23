using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Events;

public class AlertInfo : MonoBehaviour {
    private readonly int CAMERA_DEPTH_UNDER = -1;
    private readonly int CAMERA_DEPTH_OVER = 1;

    public Camera characterViewCamera;

    [SerializeField] private TextMeshProUGUI alertMessage;
    [SerializeField] private Button checkBtn;

    private void OnEnable() {
        if(!(characterViewCamera is null)) {
            characterViewCamera.depth = CAMERA_DEPTH_UNDER;
        }
    }

    private void OnDisable() {
        if (!(characterViewCamera is null)) {
            characterViewCamera.depth = CAMERA_DEPTH_OVER;
        }
    }

    public void SetAlertMessage(string message) {
        alertMessage.text = message;
    }

    public void ResetBtnListener(UnityAction CheckAction) {
        try {
            checkBtn.onClick.RemoveAllListeners();
        } catch (Exception e) {

        } finally {
            checkBtn.onClick.AddListener(() => {
                this.gameObject.SetActive(false);
            });
            checkBtn.onClick.AddListener(CheckAction);
        }
    }
}