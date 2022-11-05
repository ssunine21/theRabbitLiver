using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour, IStoreListener {
    private readonly string Premium = "premium";
    private readonly string AllPackage = "all_package";
    private readonly string CharacterPackage = "character_package";
    private readonly string CoinBox = "coin_box";
    private readonly string CoinDummy = "coin_dummy";


    public readonly string and_Premium = "premium";
    public readonly string and_AllPackage = "all_package";
    public readonly string and_CharacterPackage = "character_package";
    public readonly string and_CoinBox = "coin_box";
    public readonly string and_CoinDummy = "coin_dummy";

    private IStoreController storeController;
    private IExtensionProvider extensionProvider;
    private bool isInit = false;

    private Dictionary<string, string> specialProductsPriceTextDic = new Dictionary<string, string>();

    private void Awake() {
        Singleton();
    }

    private void Start() {
        if (isInit) return;
        InitIAP();
    }

    public string GetSpecialProductsPrice(string key) {
        return specialProductsPriceTextDic[key];
    }

    private void SetSpecialProductsPrice() {
        if (specialProductsPriceTextDic == null)
            specialProductsPriceTextDic = new Dictionary<string, string>();

        specialProductsPriceTextDic.Add(Premium, storeController.products.WithID(Premium).metadata.localizedPrice.ToString());
        specialProductsPriceTextDic.Add(AllPackage, storeController.products.WithID(AllPackage).metadata.localizedPrice.ToString());
        specialProductsPriceTextDic.Add(CharacterPackage, storeController.products.WithID(CharacterPackage).metadata.localizedPrice.ToString());
        specialProductsPriceTextDic.Add(CoinBox, storeController.products.WithID(CoinBox).metadata.localizedPrice.ToString());
        specialProductsPriceTextDic.Add(CoinDummy, storeController.products.WithID(CoinDummy).metadata.localizedPrice.ToString());
    }

    private void InitIAP() {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(Premium, ProductType.NonConsumable, new IDs() {
            {and_Premium, GooglePlay.Name }
        });
        builder.AddProduct(AllPackage, ProductType.Consumable, new IDs() {
            {and_AllPackage, GooglePlay.Name }
        });
        builder.AddProduct(CharacterPackage, ProductType.Consumable, new IDs() {
            {and_CharacterPackage, GooglePlay.Name }
        });
        builder.AddProduct(CoinBox, ProductType.Consumable, new IDs() {
            {and_CoinBox, GooglePlay.Name }
        });
        builder.AddProduct(CoinDummy, ProductType.Consumable, new IDs() {
            {and_CoinDummy, GooglePlay.Name }
        });

        UnityPurchasing.Initialize(this, builder);
    }

    void IStoreListener.OnInitialized(IStoreController controller, IExtensionProvider extensions) {
        storeController = controller;
        extensionProvider = extensions;

        SetSpecialProductsPrice();

        if (storeController.products.WithID(Premium).hasReceipt) {
            DataManager.init.DeviceData.isPremium = 1;
            DataManager.init.DeviceData.Save();
        }

        isInit = true;
    }

    void IStoreListener.OnInitializeFailed(InitializationFailureReason error) {
        
    }

    void IStoreListener.OnPurchaseFailed(Product product, PurchaseFailureReason failureReason) {
    }

    PurchaseProcessingResult IStoreListener.ProcessPurchase(PurchaseEventArgs purchaseEvent) {
        if(purchaseEvent.purchasedProduct.definition.id == and_AllPackage) {
            DataManager.init.CloudData.AllCharacterUnlock();
            DataManager.init.CoinEarn(20000);
        } else if (purchaseEvent.purchasedProduct.definition.id == and_CharacterPackage) {
            DataManager.init.CloudData.AllCharacterUnlock();
        }  else if (purchaseEvent.purchasedProduct.definition.id == and_CoinBox) {
            DataManager.init.CoinEarn(15000);
        } else if (purchaseEvent.purchasedProduct.definition.id == and_CoinDummy) {
            DataManager.init.CoinEarn(5000);
        } else if (purchaseEvent.purchasedProduct.definition.id == and_Premium) {
            DataManager.init.DeviceData.isPremium = 1;
            DataManager.init.DeviceData.Save();
        }

        return PurchaseProcessingResult.Complete;
    }



    public void Purchase(string productId) {
        if (!isInit) return;

        var product = storeController.products.WithID(productId);
        if(product != null && product.availableToPurchase) {
            storeController.InitiatePurchase(product);
        }
    }

    public static IAPManager init;
    private void Singleton() {
        if (init == null) {
            init = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(this.gameObject);
        }
    }
}