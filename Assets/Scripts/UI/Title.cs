using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour {
    private readonly int hashTitlePlay = Animator.StringToHash("Title");

    Animator anim;
    bool isFirstStart = true;

    private void Awake() {
        anim = GetComponent<Animator>();
    }

    private void OnEnable() {
/*        if(isFirstStart) {
            isFirstStart = false;
            return;
        }

        Play();*/
    }

    public void Play() {
        UIManager.init.NoTouchPanel.SetActive(true);
        anim.Play(hashTitlePlay);
    }

    public void OnTouch() {
        UIManager.init.NoTouchPanel.SetActive(false);
    }

}