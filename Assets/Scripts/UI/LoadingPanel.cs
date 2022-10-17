using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingPanel : MonoBehaviour
{
    private Animation anim;
    private AnimationClip clip;

    private void Awake() {
        anim = GetComponentInChildren<Animation>();
    }

    private void OnEnable() {
        if(anim != null) {
        }
    }
}
