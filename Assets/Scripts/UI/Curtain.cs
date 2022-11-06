using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Curtain : MonoBehaviour {
    private float _fadeSpeed = 6f;
    private Image _image;

    private void Awake() {
        _image = GetComponent<Image>();
    }

    public IEnumerator CoFadeOut() {
        //Fade out
        float time = 0;
        while (time < 1) {
            _image.color = new Color(0, 0, 0, time);
            time += Time.deltaTime * _fadeSpeed;

            yield return null;
        }
        _image.color = new Color(0, 0, 0, 1);
    }

    public IEnumerator CoFadeIn() {
        //Fade out
        float time = 2;
        while (time > 0) {
            _image.color = new Color(0, 0, 0, time);
            time -= Time.deltaTime * _fadeSpeed;

            yield return null;
        }
        _image.color = new Color(0, 0, 0, 0);
    }

    public IEnumerator CoFadeInOut(Action action) {

        StartCoroutine(CoFadeOut());
        yield return new WaitForSeconds(.5f);
        action.Invoke();
        StartCoroutine(CoFadeIn());
    }

    public void FadeInOut(Action action) {
        StartCoroutine(CoFadeInOut(action));
    }

    public void StartFadeIn() {
        StartCoroutine(CoFadeIn());
    }
}
