using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudData {
    public class CharacterProductInfo {
        public string Name {
            get; set;
        }
        private readonly bool isPurchase;
        private readonly int skillLevel;

        public CharacterProductInfo(string name, bool isPurchase, int skillLevel) {
            //this.name = name;
            this.isPurchase = isPurchase;
            this.skillLevel = skillLevel;
        }
    }

    public readonly ArrayList characterProductInfoList = new ArrayList();

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