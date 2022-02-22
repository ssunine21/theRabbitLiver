using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RestartCount : MonoBehaviour {
    public TextMeshProUGUI txtCount;

    private void OnEnable() {
        StartCoroutine(nameof(CountStart));
    }

    IEnumerator CountStart() {
        int time = 3;
        while(time > 0) {
            txtCount.text = time--.ToString();
            yield return new WaitForSeconds(1f);
        }

        GameManager.init.player.GetComponent<Player>().reverseWhenEndDeathAnimation();
        this.gameObject.SetActive(false);
    }
}