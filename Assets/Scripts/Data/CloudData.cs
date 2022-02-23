using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudData {
    public class CharacterProductInfo {
        public DeviceData.CharacterID Name { get; set; }
        public bool IsPurchase { get; set; }
        public int SkillLevel { get; set; }

        public CharacterProductInfo(DeviceData.CharacterID name, bool isPurchase, int skillLevel) {
            this.Name = name;
            this.IsPurchase = isPurchase;
            this.SkillLevel = skillLevel;
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
        CharacterProductInfo tempInfo = new CharacterProductInfo(DeviceData.CharacterID.bunny, true, 3);
        CharacterProductInfo tempInfo1 = new CharacterProductInfo(DeviceData.CharacterID.skeleton, false, 5);
        CharacterProductInfo tempInfo2 = new CharacterProductInfo(DeviceData.CharacterID.bono, false, 0);
        CharacterProductInfo tempInfo3 = new CharacterProductInfo(DeviceData.CharacterID.notake, true, 0);

        characterProductInfoList.Add(tempInfo);
        characterProductInfoList.Add(tempInfo1);
        characterProductInfoList.Add(tempInfo2);
        characterProductInfoList.Add(tempInfo3);
    }
    private void LoadCoinData() {
        coin = 2000;
    }
}