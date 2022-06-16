using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Item, IItem {
    public void Use() {
        GameManager.init.recordData.itemCount++;
        GameManager.init.recordData.coin += 15;
        Destroy(this.gameObject);
    }
}