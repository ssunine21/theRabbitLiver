using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exclamation : MonoBehaviour
{
    Button _exclamationBtn;
    Image _exclamationImg;
    bool _isInfoPanelOn = false;
    public GameObject _infoPanel;
    public Sprite _exclamation;
    public Sprite _close;

    Animator _anim;

    private void Awake() {
        _exclamationBtn = GetComponent<Button>();
        _exclamationImg = GetComponentInChildren<Image>();
    }

    private void Start() {
        _exclamationBtn.onClick.AddListener(OnExclamationBtnClick);
    }

    private  void OnExclamationBtnClick() {
        _exclamationImg.sprite = _isInfoPanelOn ? _close : _exclamation;
        _infoPanel.SetActive(_isInfoPanelOn);
        _isInfoPanelOn = !_isInfoPanelOn;
    }
}
