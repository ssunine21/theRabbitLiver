using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ItemBtn : MonoBehaviour {
    private readonly float UI_FADEOUT_TIME = 5f;

    [SerializeField] private DeviceData.ItemID itemName;
    [SerializeField] private Image btnImg;

    public void OnPlay() {
        if(DataManager.init.CloudData.itemProductInfoList[itemName].count <= 0) {
            this.GetComponent<Button>().interactable = false;
            btnImg.color = new Color(1, 1, 1, 0.3f);

            if (itemName == DeviceData.ItemID.skip) {
                gameObject.SetActive(false);
            }
        } else {
            this.GetComponent<Button>().interactable = true;
            btnImg.color = new Color(1, 1, 1, 1);

            if (itemName == DeviceData.ItemID.skip) {
                gameObject.SetActive(true);
                Invoke(nameof(SetActiveFalse), 8f);
            }
        }
    }

    public void Use() {
        DataManager.init.CloudData.itemProductInfoList[itemName].count -= 1;
        DataManager.init.CloudData.DataAysnc(itemName.ToString(), DataManager.init.CloudData.itemProductInfoList[itemName].count);
        this.GetComponent<Button>().interactable = false;
        btnImg.color = new Color(1, 1, 1, 0.3f);
    }

    private void SetActiveFalse() {
        gameObject.SetActive(false);
    }
}