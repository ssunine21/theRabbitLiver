using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Coin : MonoBehaviour {
    
    [SerializeField] private TextMeshProUGUI coinText;
    public int coin {
        get {
            return int.TryParse(coinText.text, out int result) == true ? result : 0;
        }
        set {
            if (value < 0) value = 0;
            coinText.text = value.ToString();
        }
    }

    private void OnEnable() {
        coinText.text = DataManager.init.CloudData._coin.ToString();
    }

    private void OnDisable() {
        DataManager.init.CloudData._coin = coin;
    }
}