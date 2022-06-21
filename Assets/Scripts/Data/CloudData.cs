using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudData {
    public class ItemProductInfo {
        public int itemLevel { get; set; }
        public int[] price { get; set; }

        private int[] _percentage;
        public int percentage {
            get => _percentage[itemLevel];
        }

        public int count;

        public ItemProductInfo(int itemLevel, int[] price, int[] percentage, int count = 1) {
            this.itemLevel = itemLevel;
            this.price = price;
            this._percentage = percentage;
            this.count = count;
        }
    }

    public readonly Dictionary<DeviceData.CharacterID, int> characterLevel = new Dictionary<DeviceData.CharacterID, int>();
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
        characterLevel.Add(DeviceData.CharacterID.bunny, 4);
        characterLevel.Add(DeviceData.CharacterID.skeleton, 5);
        characterLevel.Add(DeviceData.CharacterID.bono, 1);
        characterLevel.Add(DeviceData.CharacterID.jazz, 0);
        characterLevel.Add(DeviceData.CharacterID.notake, 0);
    }

    private void LoadItemProductInfo() {
        itemProductInfoList.Add(DeviceData.ItemID.coinplus,
            new ItemProductInfo(0, new int[5] { 1000, 2000, 3000, 4000, 5000 }, new int[5] { 20, 25, 30, 35, 40 }));
        itemProductInfoList.Add(DeviceData.ItemID.heartplus,
            new ItemProductInfo(0, new int[5] { 1000, 2000, 3000, 4000, 5000 }, new int[5] { 20, 25, 30, 35, 40 }));
        itemProductInfoList.Add(DeviceData.ItemID.protectionplus,
            new ItemProductInfo(0, new int[5] { 1000, 2000, 3000, 4000, 5000 }, new int[5] { 5, 10, 12, 15, 17 }));
        itemProductInfoList.Add(DeviceData.ItemID.heart,
            new ItemProductInfo(0, new int[5] { 1000, 2000, 3000, 4000, 5000 }, new int[5] { 5, 10, 12, 15, 17 }));
        itemProductInfoList.Add(DeviceData.ItemID.protection,
            new ItemProductInfo(0, new int[5] { 1000, 2000, 3000, 4000, 5000 }, new int[5] { 5, 10, 12, 15, 17 }));
        itemProductInfoList.Add(DeviceData.ItemID.skip,
            new ItemProductInfo(0, new int[5] { 1000, 2000, 3000, 4000, 5000 }, new int[5] { 5, 10, 12, 15, 17 }));
    }

    private void LoadCoinData() {
        coin = 20000;
    }
}