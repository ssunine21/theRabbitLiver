using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudData {
    public class CharacterProductInfo {
        public DeviceData.CharacterID name { get; set; }
        public bool isPurchase { get; set; }
        public int skillLevel { get; set; }
        public int price { get; set; }

        public CharacterProductInfo(DeviceData.CharacterID name, bool isPurchase, int skillLevel, int price) {
            this.name = name;
            this.isPurchase = isPurchase;
            this.skillLevel = skillLevel;
            this.price = price;
        }
    }

    public class OtherProductInfo {
        public bool isPurchase { get; set; }
        public bool itemLevel { get; set; }
        public int[] price { get; set; }

        public OtherProductInfo() {
            price = new int[] { };
        }
    }

    public readonly List<CharacterProductInfo> characterProductInfoList = new List<CharacterProductInfo>();
    private int _coin;
    public int coin { get {
            return _coin;
        } set {
            _coin = value;
        }
    }

    public void Load() {
        LoadCharacterProductInfo();
        LoadCoinData();
    }

    public void Save() {
    }

    private void LoadCharacterProductInfo() {
        CharacterProductInfo tempInfo = new CharacterProductInfo(DeviceData.CharacterID.bunny, true, 1, 1000);
        CharacterProductInfo tempInfo1 = new CharacterProductInfo(DeviceData.CharacterID.skeleton, false, 0, 1500);
        CharacterProductInfo tempInfo2 = new CharacterProductInfo(DeviceData.CharacterID.bono, false, 0, 1012);
        CharacterProductInfo tempInfo3 = new CharacterProductInfo(DeviceData.CharacterID.notake, false, 0, 1039);

        characterProductInfoList.Add(tempInfo);
        characterProductInfoList.Add(tempInfo1);
        characterProductInfoList.Add(tempInfo2);
        characterProductInfoList.Add(tempInfo3);
    }

    private void LoadOtherProductInfo() {

    }
    private void LoadCoinData() {
        coin = 20000;
    }
}