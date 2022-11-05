using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OnOff : MonoBehaviour {
    enum btn {
        On, Off
    }

    public GameObject[] panelList;
    public GameObject[] btnList;

    private string[] AnimArray = { "On", "Off" };
    private int preIndex = 0;
    private bool isOn = false;
    private Animation childAnim;

    private void Start() {
        childAnim = this.GetComponentInChildren<Animation>();
    }

    public void OnClick(int index) {
        if (preIndex == index) return;
        SoundManager.init.PlaySFXSound(Definition.SoundType.ButtonClick);
        BtnStateChange(index);
        PanelStateChange(index);
        preIndex = index;
    }

    private void BtnStateChange(int index) {
        btnList[preIndex].GetComponent<Animation>().Play(AnimArray[(int)btn.Off]);
        btnList[index].GetComponent<Animation>().Play(AnimArray[(int)btn.On]);
    }

    private void PanelStateChange(int index) {
        panelList[preIndex].SetActive(false);
        panelList[index].SetActive(true);
    }
}