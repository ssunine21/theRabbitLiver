using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour {

    public Image hpBar;

    private void FixedUpdate() {
        hpBar.fillAmount -= SpawnManager.init.levelDesign.hpDecreasingSpeed * Time.deltaTime;
    }

}