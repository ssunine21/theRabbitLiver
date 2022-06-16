using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animation))]
public class AnimationControll : MonoBehaviour {
    private Vector2 offsetMin;
    private Vector2 offsetMax;
    private RectTransform rect;
    private Animation anim;
    public float delay;

    private void Start() {
        anim = this.GetComponent<Animation>();
        rect = this.GetComponent<RectTransform>();
    }

    private void OnEnable() {
        if (rect is null) rect = GetComponent<RectTransform>();
        offsetMax = rect.offsetMax;
        offsetMin = rect.offsetMin;
        Invoke("StartAnimation", delay);
    }

    private void OnDisable() {
        rect.offsetMin = offsetMin;
        rect.offsetMax = offsetMax;
    }

    private void StartAnimation() {
        try {
            anim.Play(anim.clip.name);
        } catch (NullReferenceException NRE) {
#if (DEBUG)
            UnityEngine.Debug.LogError(NRE.Message);
#endif
        }
    }
}