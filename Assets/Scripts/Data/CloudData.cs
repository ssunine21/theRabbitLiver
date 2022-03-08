using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudData {
    public class CharacterProductInfo {
        public bool isPurchase { get; set; }
        public int skillLevel { get; set; }
        public int price { get; }
        private int[] _levelPrice;
        public int levelPrice {
            get {
                return _levelPrice[skillLevel];
            }
        }

        public CharacterProductInfo(bool isPurchase, int skillLevel, int price, int[] levelPrice) {
            this.isPurchase = isPurchase;
            this.skillLevel = skillLevel;
            this.price = price;
            this._levelPrice = levelPrice;
        }
    }

    public class ItemProductInfo {
        public bool isPurchase { get; set; }
        public int itemLevel { get; set; }
        public int[] price { get; set; }

        public ItemProductInfo(bool isPurchase, int itemLevel, int[] price) {
            this.isPurchase = isPurchase;
            this.itemLevel = itemLevel;
            this.price = price;
        }
    }

    public readonly Dictionary<DeviceData.CharacterID, CharacterProductInfo> characterProductInfoList = new Dictionary<DeviceData.CharacterID, CharacterProductInfo>();
    public readonly Dictionary<DeviceData.ItemID, ItemProductInfo> itemProductInfoList = new Dictionary<DeviceData.ItemID, ItemProductInfo>();
    private int _coin;
    public int coin {
        get {
            return _coin;
        } set {
            _coin = value;
        }
    }

    public void Load() {
        LoadCharacterProductInfo();
        LoadItemProductInfo();
        LoadCoinData();
    }

    public void Save() {
    }

    private void LoadCharacterProductInfo() {
        characterProductInfoList.Add(DeviceData.CharacterID.bunny, new CharacterProductInfo(true, 1, 1000, new int[4] { 1000, 2000, 3000, 4000 }));
        characterProductInfoList.Add(DeviceData.CharacterID.skeleton, new CharacterProductInfo(false, 0, 1500, new int[4] { 1000, 2000, 3000, 4000 }));
        characterProductInfoList.Add(DeviceData.CharacterID.bono, new CharacterProductInfo(false, 0, 1012, new int[4] { 1000, 2000, 3000, 4000 }));
        characterProductInfoList.Add(DeviceData.CharacterID.notake, new CharacterProductInfo(false, 0, 1039, new int[4] { 1000, 2000, 3000, 4000 }));
    }

    private void LoadItemProductInfo() {
        itemProductInfoList.Add(DeviceData.ItemID.coinplus, new ItemProductInfo(false, 0, new int[5] {1000, 2000, 3000, 4000, 5000}));
        itemProductInfoList.Add(DeviceData.ItemID.heartplus, new ItemProductInfo(false, 0, new int[5] { 1000, 2000, 3000, 4000, 5000 }));
        itemProductInfoList.Add(DeviceData.ItemID.protectionplus, new ItemProductInfo(false, 0, new int[5] { 1000, 2000, 3000, 4000, 5000 }));
        itemProductInfoList.Add(DeviceData.ItemID.protection, new ItemProductInfo(false, 0, new int[5] { 2000, 2000, 2000, 2000, 2000 }));
    }

    private void LoadCoinData() {
        coin = 20000;
    }
}