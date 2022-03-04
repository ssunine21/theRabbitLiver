using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour {
    [SerializeField] private DeviceData.ItemID itemName;
    [SerializeField] private Transform levelBar;
    [SerializeField] private Button btnBuy;

    private CloudData.ItemProductInfo infoData;
    private int level;

    private void OnEnable() {
        infoData = DataManager.init.CloudData.itemProductInfoList[this.itemName];
        this.level = infoData.itemLevel;

        AddBuyBtnListener();
        SetLevelBar(level);
    }

    private void SetLevelBar(int level) {
        if (levelBar == null) return;
        for(int i = 0; i < level; ++i) {
            levelBar.GetChild(i).GetComponent<RawImage>().color = Color.white;
        }
    }

    private void BuyItem() {

    }

    private void AddBuyBtnListener() {
        try {
            btnBuy.onClick.RemoveAllListeners();
        } catch(Exception e) {
            
        }
        //btnBuy.onClick.AddListener(BuyItem);
    }

    //private void BuyItem() {
    //    if (level >= levelBar.childCount) {
    //        UIManager.init.ShowAlert(Definition.BUY_ENOUGH);
    //    } else {
    //        UIManager.init.ShowAlert(Definition.BUY_MASSAGE, LevelUp, BtnNextCharacter);
    //    }
    //}
}