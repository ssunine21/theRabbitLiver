using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemInfo : MonoBehaviour {
    [SerializeField] private DeviceData.ItemID itemName;
    [SerializeField] private Transform levelBar;
    [SerializeField] private Button btnBuy;
    [SerializeField] private TextMeshProUGUI _textCoinPrice;
    private int coinPrice {
        set { _textCoinPrice.text = value.ToString(); }
    }

    private enum Type {
        single, multiple
    }

    [SerializeField]private Type type;

    private CloudData.ItemProductInfo infoData;
    private Transform[] purchaseBtnAndIsPurchaseText = new Transform[2];
    private int level {
        get { return infoData == null ? 0 : infoData.itemLevel; }
        set { infoData.itemLevel = value; }
    }

    private void OnEnable() {
        infoData = DataManager.init.CloudData.itemProductInfoList[this.itemName];
        purchaseBtnAndIsPurchaseText[0] = btnBuy.transform.GetChild(0).GetChild(0);
        purchaseBtnAndIsPurchaseText[1] = btnBuy.transform.GetChild(0).GetChild(1);
        ChangeItemPrice();

        if (IsPurchase()) {
            OffBtnFunction();
            return;
        }
        AddBuyBtnListener();
        SetLevelBar(level);
    }

    private void OffBtnFunction() {
        btnBuy.interactable = false;
        infoData.isPurchase = true;
        ChangeBtnText();
    }

    private bool IsPurchase() {
        return infoData.isPurchase;
    }

    private void ChangeBtnText() {
        purchaseBtnAndIsPurchaseText[0].gameObject.SetActive(false);
        purchaseBtnAndIsPurchaseText[1].gameObject.SetActive(true);
    }

    private void ChangeItemPrice() {
        coinPrice = infoData.price[level]; 
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
        UIManager.init.ShowAlert(Definition.BUY_MASSAGE, BuyItem, null);
    }

    private void BuyItem() {
        if (DataManager.init.CoinComparison(infoData.price[level++], true)) {
            if (type == Type.multiple) {
                SetLevelBar(level);
                if (level >= levelBar.childCount) {
                    OffBtnFunction();
                } else {
                    ChangeItemPrice();
                }
            } else {
                OffBtnFunction();
            }
        } else {
            UIManager.init.ShowAlert(Definition.NOT_ENOUGH_MONEY, null);
        }
    }
}