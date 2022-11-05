using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Item, IItem {
    [Range(0.0f, 1f)]
    public float AMOUNT_RECOVERY_HP_ON_HEALTH_ITEM;

    public void Use() {

        SoundManager.init.PlaySFXSound(Definition.SoundType.Item_Health);
        GameManager.init.recordData.itemCount++;
        player.stamina.hpBar += AMOUNT_RECOVERY_HP_ON_HEALTH_ITEM;
        player.isHeal = true;
    }
}