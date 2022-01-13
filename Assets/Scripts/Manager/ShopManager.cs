using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour {

    private int coin;
    public int Coin {
        get { return coin; }
        set {
            coin = value;
        }
    }

    private void OnEnable() {
        Coin = DataManager.init.CloudData.coin;
    }


    public static ShopManager init;
    private void Singleton() {
        if (init == null) {
            init = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(this.gameObject);
        }
    }

}