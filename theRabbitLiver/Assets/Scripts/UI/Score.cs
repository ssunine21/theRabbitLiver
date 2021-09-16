using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour {

    [SerializeField]
    private TextMeshProUGUI _currScore;

    [SerializeField]
    private TextMeshProUGUI _bestScore;

    public int currScore {
        get {
            int.TryParse(_currScore.text, out int number);
            return number;
        }
        set {
            if (value < 0) value = 0;
            _currScore.text = value.ToString();
        }
    }
    public int bestScore {
        get {
            int.TryParse(_bestScore.text, out int number);
            return number;
        }
        set {
            if (value < 0) value = 0;
            _bestScore.text = value.ToString();
        }
    }

}