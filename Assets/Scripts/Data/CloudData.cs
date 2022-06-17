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
        private int[] _hpincrease;
        public int hpincrease {
            get {
                return _hpincrease[skillLevel == 0 ? skillLevel : skillLevel - 1];
            }
        }
        private int[] _skillCount;
        public int skillCount {
            get {
                return _skillCount[skillLevel == 0 ? skillLevel : skillLevel - 1];
            }
        }

        public CharacterProductInfo(bool isPurchase, int skillLevel, int price, int[] levelPrice, int[] hpincrease, int[] skillCount) {
            this.isPurchase = isPurchase;
            this.skillLevel = skillLevel;
            this.price = price;
            this._levelPrice = levelPrice;
            this._hpincrease = hpincrease;
            this._skillCount = skillCount;
        }
    }

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
        characterProductInfoList.Add(DeviceData.CharacterID.bunny, new CharacterProductInfo(true, 1, 1000,
            new int[5] { 500, 1000, 2000, 3000, 4000 }, new int[5] { 10, 20, 30, 40, 50 }, new int[5] { 1, 1, 2, 2, 3 }));
        characterProductInfoList.Add(DeviceData.CharacterID.skeleton, new CharacterProductInfo(false, 0, 1500,
            new int[5] { 500, 1000, 2000, 3000, 4000 }, new int[5] { 11, 21, 31, 41, 51 }, new int[5] { 1, 1, 2, 2, 3 }));
        characterProductInfoList.Add(DeviceData.CharacterID.bono, new CharacterProductInfo(false, 0, 1012,
            new int[5] { 500, 1000, 2000, 3000, 4000 }, new int[5] { 12, 22, 32, 42, 52 }, new int[5] { 1, 1, 2, 2, 3 }));
        characterProductInfoList.Add(DeviceData.CharacterID.jazz, new CharacterProductInfo(false, 0, 1039,
            new int[5] { 500, 1000, 2000, 3000, 4000 }, new int[5] { 13, 23, 33, 43, 53 }, new int[5] { 1, 1, 2, 2, 3 }));
        characterProductInfoList.Add(DeviceData.CharacterID.notake, new CharacterProductInfo(false, 0, 1039,
            new int[5] { 500, 1000, 2000, 3000, 4000 }, new int[5] { 14, 24, 34, 44, 54 }, new int[5] { 1, 1, 2, 2, 3 }));
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