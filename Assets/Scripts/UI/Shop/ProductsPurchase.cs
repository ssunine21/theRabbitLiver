using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductsPurchase : MonoBehaviour {
    public string productId;

    [SerializeField]
    private Button buyBtn;

    private void Start() {
        buyBtn = GetComponentInChildren<Button>();
        buyBtn.onClick.AddListener(OnClick);
    }

    private void OnClick() {
        IAPManager.init.Purchase(productId);
    }

}
