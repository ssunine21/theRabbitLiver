using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeepSeaTile : MonoBehaviour {
    public Material material;
    //92A3AD light  0.58 0.65 0.75
    //525B6A dark   0.32 0.36 0.42

    private void Start() {
        material.SetColor("_SpecColor", new Color(0.32f, 0.36f, 0.42f));
    }
}