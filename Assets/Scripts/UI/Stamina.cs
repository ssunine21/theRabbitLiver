using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour {
    const float MAX_STAMINA = 1;
    const float MIN_STAMINA = 0;

    [SerializeField]
    private Image _hpBar;
    public float hpBar {
        get { return _hpBar.fillAmount; }
        set {
            _hpBar.fillAmount = value;
            if (_hpBar.fillAmount <= 0) {
                _hpBar.fillAmount = 0;
            } else if (_hpBar.fillAmount > 1)
                _hpBar.fillAmount = 1;
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

            isSkillOn(_mpBar.fillAmount >= 1);
        }
    }

    [SerializeField]
    private Sprite skillOn;

    [SerializeField]
    private Sprite skillOff;

    [SerializeField]
    private Image skillImg;

    private void isSkillOn(bool isOn) {
        if (isOn) skillImg.sprite = skillOn;
        else skillImg.sprite = skillOff;
    }

    public void SetStamina(float hp = MAX_STAMINA, float mp = MIN_STAMINA) {
        hpBar = hp;
        mpBar = mp;
    }

    public void OnSkill() {
        if (mpBar >= MAX_STAMINA) {
            GameManager.init.player.GetComponent<Player>().Skill();
            mpBar = MIN_STAMINA;
        }
    }
}