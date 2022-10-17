using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RestartCount : MonoBehaviour {
    public TextMeshProUGUI txtCount;

    private IEnumerator coCount = null;
    private int time = 3;

    public void Count(Action action) {
        time = 3;

        if (coCount != null) {
            StopCoroutine(coCount);
            coCount = null;
        }
        coCount = CoCount(action);

        StartCoroutine(CoCount(action));
    }

    IEnumerator CoCount(Action action) {
        while(time > 0) {
            txtCount.text = time--.ToString();
            yield return new WaitForSeconds(1f);
        }

        time = 3;
        txtCount.text = "";
        action.Invoke();
    }
}