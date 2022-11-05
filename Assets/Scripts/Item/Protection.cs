using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Protection : Item, IItem {
    public void Use() {

        SoundManager.init.PlaySFXSound(Definition.SoundType.Item_Shield);
        GameManager.init.recordData.itemCount++;
        player.OnProtection();
    }
}