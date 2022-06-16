using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchClose : MonoBehaviour {
    public Animation lastObject;
    
    private void Update() {
        if(lastObject.isPlaying) {
            if(Input.GetMouseButtonDown(0)) {
                UIManager.init.ToMainFromInGame();
            }
        }
    }
}