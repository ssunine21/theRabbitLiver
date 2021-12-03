using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterView : MonoBehaviour {

    public GameObject[] characters;
    public Vector3 pos;

    private int preIndex = 0;
    private int _index = 0;
    public int index {
        get { return _index; }
        set {
            if (value < 0) value = characters.Length - 1;
            else if (value >= characters.Length) value = 0;

            _index = value;
        }
    }

    private void Start() {
        characters[index].transform.localPosition = pos;
    }

    public void ButtnClick(bool isRight) {
        preIndex = index;
        index += isRight ? 1 : -1;

        ShowCharacterChange();
    }

    private void ShowCharacterChange() {
        characters[preIndex].transform.localPosition = Vector3.one * 100;
        characters[index].transform.localPosition = pos;
    }
}