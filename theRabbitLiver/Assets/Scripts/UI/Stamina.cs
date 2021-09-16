using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour {

    [SerializeField]
    private Image _hpBar;
    public float hpBar {
        get { return _hpBar.fillAmount; }
        set {
            _hpBar.fillAmount = value;
        }
    }

    [SerializeField]
    private Image _mpBar;
    public float mpBar {
        get { return _mpBar.fillAmount; }
        set {
            _mpBar.fillAmount = value;
            if (_mpBar.fillAmount < 0) _mpBar.fillAmount = 0;
            else if (_mpBar.fillAmount > 1) _mpBar.fillAmount = 1;
        }
    }

    private void FixedUpdate() {
        _hpBar.fillAmount -= SpawnManager.init.levelDesign.hpDecreasingSpeed * Time.deltaTime;
    }

    public void SetStamina(float hp = 1, float mp = 0) {
        hpBar = hp;
        mpBar = mp;
    }
}