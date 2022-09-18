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

    [SerializeField] private TextMeshProUGUI _textCurrItemCount;
    private int _currItemCount;
    public int currItemCount {
        get { return _currItemCount; }
        set {
            try {
                _currItemCount = value;
                _textCurrItemCount.text = Definition.CURR_ITEM_COUNT + value.ToString();
            } catch (NullReferenceException NRE) {
#if DEBUG
                UnityEngine.Debug.LogError(NRE.Message);
#endif
            }
        }
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
        ChangeItemCountText();

        AddBuyBtnListener();
        SetLevelBar(level);
    }

    private void OffBtnFunction() {
        btnBuy.interactable = false;
        ChangeBtnText();
    }

    private void ChangeBtnText() {
        purchaseBtnAndIsPurchaseText[0].gameObject.SetActive(false);
        purchaseBtnAndIsPurchaseText[1].gameObject.SetActive(true);
    }

    private void ChangeItemCountText() {
        if (type == Type.single)
            currItemCount = infoData.count;
    }

    private void SingleItemPruchase() {
        infoData.count += 1;
        ChangeItemCountText();
    }

    private void ChangeItemPrice() {
        if (level >= infoData.price.Length)
            OffBtnFunction();
        else {
            coinPrice = infoData.price[level];
        }
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
#if (DEBUG)
            UnityEngine.Debug.Log(e.Message);
#endif
        }
        btnBuy.onClick.AddListener(ShowBuyMessage);
    }

    private void ShowBuyMessage() {
        UIManager.init.ShowAlert(Definition.BUY_MASSAGE, BuyItem, null);
    }

    private void BuyItem() {
        if (DataManager.init.CoinComparison(infoData.price[level], true)) {
            if (type == Type.multiple) {
                level += 1;
                if (level > levelBar.childCount) {
                    OffBtnFunction();
                } else {
                    ChangeItemPrice();
                    SetLevelBar(level);
                }
            } else {
                SingleItemPruchase();
            }
        } else {
            UIManager.init.ShowAlert(Definition.NOT_ENOUGH_MONEY, null);
        }
    }
}