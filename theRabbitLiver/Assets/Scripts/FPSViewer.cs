using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSViewer : MonoBehaviour {
    [Range(1, 100)]
    public int fontSize;
    [Range(0, 1)]
    public float R, G, B;

    private float deltaTime = 0f;

    private void Start() {
        fontSize = fontSize == 0 ? 50 : fontSize;
    }

    private void Update() {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }

    private void OnGUI() {
        int w = Screen.width;
        int h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 0, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 2 / fontSize;
        style.normal.textColor = new Color(R, G, B, 1f);
        float msec = deltaTime * 1000f;
        float fps = 1f / deltaTime;
        string text = string.Format("{0:0.0}ms ({1:0}fps)", msec, fps);
        GUI.Label(rect, text, style);
    }

}