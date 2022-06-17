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
        } else {
            this.GetComponent<Button>().interactable = true;
            btnImg.color = new Color(1, 1, 1, 1);

            if(itemName == DeviceData.ItemID.skip) {
                StartCoroutine(nameof(UITime));
            }
        }
    }

    public void Use() {
        DataManager.init.CloudData.itemProductInfoList[itemName].count -= 1;
        this.GetComponent<Button>().interactable = false;
        btnImg.color = new Color(1, 1, 1, 0.3f);
    }

    IEnumerator UITime() {
        float currTime = UI_FADEOUT_TIME;

        while (currTime >= 0) {
            currTime -= Time.deltaTime;
            btnImg.fillAmount = (currTime / UI_FADEOUT_TIME);
            yield return null;
        }

        this.GetComponent<Button>().interactable = false;
    }
}