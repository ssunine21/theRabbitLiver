using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour {
    [SerializeField] private DeviceData.ItemID itemName;
    [SerializeField] private Transform levelBar;
    [SerializeField] private Button btnBuy;

    private enum Type {
        single, multiple
    }

    [SerializeField]private Type type;

    private CloudData.ItemProductInfo infoData;
    private int level {
        get { return infoData == null ? 0 : infoData.itemLevel; }
        set { infoData.itemLevel = value; }
    }

    private void OnEnable() {
        infoData = DataManager.init.CloudData.itemProductInfoList[this.itemName];

        AddBuyBtnListener();
        SetLevelBar(level);
    }

    private void SetLevelBar(int level) {
        if (levelBar == null) return;
        for(int i = 0; i < level; ++i) {
            levelBar.GetChild(i).GetComponent<RawImage>().color = Color.white;
        }
    }

    private void AddBuyBtnListener() {
        try {
            btnBuy.onClick.RemoveAllListeners();
        } catch(Exception e) {
            
        }
        btnBuy.onClick.AddListener(ShowBuyMessage);
    }

    private void ShowBuyMessage() {
        if (type == Type.single) return;

        if (level >= levelBar.childCount) {
            UIManager.init.ShowAlert(Definition.BUY_ENOUGH, null);
        } else {
            UIManager.init.ShowAlert(Definition.BUY_MASSAGE, BuyItem, null);
        }
    }

    private void BuyItem() {
        SetLevelBar(++level);
    }
}