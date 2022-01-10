using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Restart : MonoBehaviour {
    private readonly float RESTART_UI_TIME = 5f;

    [SerializeField] private Image reImg;
    [SerializeField] private Button coinBtn;
    [SerializeField] private Button adsBtn;


    private void OnEnable() {
        StartCoroutine(nameof(UITime));
    }

    IEnumerator UITime() {
        float currTime = RESTART_UI_TIME;

        while(currTime >= 0) {
            currTime -= Time.deltaTime;
            reImg.fillAmount = (currTime / RESTART_UI_TIME);
            yield return null;
        }
    }

    private void CompareCoin() {

    }
}
