using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudData {
    public class CharacterProductInfo {
        public string Name { get; set; }
        public bool IsPurchase { get; set; }
        public int SkillLevel { get; set; }

        public CharacterProductInfo(string name, bool isPurchase, int skillLevel) {
            this.Name = name;
            this.IsPurchase = isPurchase;
            this.SkillLevel = skillLevel;
        }
    }

    public readonly List<CharacterProductInfo> characterProductInfoList = new List<CharacterProductInfo>();

    public void Load() {
        LoadCharacterProductInfo();

    }

    public void Save() {

    }

    private void LoadCharacterProductInfo() {
        CharacterProductInfo tempInfo = new CharacterProductInfo("Bunny", true, 3);
        CharacterProductInfo tempInfo1 = new CharacterProductInfo("Skeleton", true, 1);
        CharacterProductInfo tempInfo2 = new CharacterProductInfo("Bono", false, 0);

        characterProductInfoList.Add(tempInfo);
        characterProductInfoList.Add(tempInfo1);
        characterProductInfoList.Add(tempInfo2);
    }
}