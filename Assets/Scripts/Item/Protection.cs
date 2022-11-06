using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Protection : Item, IItem {
    public void Use() {
        GameManager.init.recordData.itemCount++;
        player.OnProtection();
    }
}