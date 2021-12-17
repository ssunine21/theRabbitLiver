using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInfo : MonoBehaviour {
    public enum ItemType {
        heart, coin, enemy
    }

    public Vector3 offset;
    public ItemType itemType;

    [Range(0.0f, 1f)]
    public float AMOUNT_RECOVERY_HP_ON_HEALTH_ITEM;
    public int AMOUNT_COIN;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(Definition.TAG_PLAYER)) {
            switch (itemType) {
                case ItemType.heart:
                    other.GetComponent<Player>().stamina.hpBar += AMOUNT_RECOVERY_HP_ON_HEALTH_ITEM;
                    break;

                case ItemType.coin:

                    break;
            }
            Destroy(this.gameObject);
        }
    }

}