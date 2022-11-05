using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProductsPurchase : MonoBehaviour {
    public string productId;

    [SerializeField] private Button buyBtn;
    [SerializeField] private TextMeshProUGUI priceText;

    private void Start() {
        buyBtn = GetComponentInChildren<Button>();
        buyBtn.onClick.AddListener(OnClick);

        priceText.text = IAPManager.init.GetSpecialProductsPrice(productId);
    }

    private void OnClick() {
        IAPManager.init.Purchase(productId);
    }

}
