using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Item, IItem {
    public void Use() {
        Destroy(this.gameObject);
    }
}