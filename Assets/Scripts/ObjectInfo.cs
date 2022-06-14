using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInfo : MonoBehaviour {
    public enum ItemType {
        heart, coin, enemy, trap
    }

    public Vector3 offset;
    public ItemType itemType;
}