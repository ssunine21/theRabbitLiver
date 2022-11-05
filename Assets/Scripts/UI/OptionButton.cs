using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OptionButton : MonoBehaviour
{
    public static int[] volumOption = new int[3] {1, 1, 1};

    public int idx;

    [SerializeField] private Sprite[] _sprites;

    private Button _btn;
    private Image _image;

    private bool _isOn = true;

    private void Awake() {
        _btn = GetComponent<Button>();
        _image = GetComponent<Image>();
    }

    private void Start() {
        _btn.onClick.AddListener(OnClickOption);

        volumOption[0] = PlayerPrefs.GetInt("SFXVolum", 1);
        volumOption[1] = PlayerPrefs.GetInt("BGMVolum", 1);
        volumOption[2] = PlayerPrefs.GetInt("Viberation", 1);

        ChangeSprite(volumOption[idx]);

        //if(PlayerPrefs.GetInt)
    }

    private void OnClickOption() {
        _isOn = !_isOn;

        var value = _isOn ? 1 : 0;
        volumOption[idx] = value;

        switch (idx) {
            case 0:
                SoundManager.init.SFXVolume = value;
                break;
            case 1:
                SoundManager.init.BGMVolume = value;
                break;
            case 2:
                break;
        }

        ChangeSprite(value);

        SoundManager.init.PlaySFXSound(Definition.SoundType.ButtonClick);
        DataManager.init.DeviceData.Save();
    }

    private void ChangeSprite(int index) {

        _image.sprite = _sprites[index];

    }
}
