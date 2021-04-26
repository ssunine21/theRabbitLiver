using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour {

    public Image _hpBar;
    public float hpBar {
        get { return _hpBar.fillAmount; }
        set {
            _hpBar.fillAmount = value;
        }
    }

    private void FixedUpdate() {
        _hpBar.fillAmount -= SpawnManager.init.levelDesign.hpDecreasingSpeed * Time.deltaTime;
    }
}