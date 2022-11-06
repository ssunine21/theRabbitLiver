using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chandelier : MonoBehaviour
{
    private Transform tr;
    private Vector3 originPos;

    private void Awake() {
        tr = GetComponent<Transform>();
    }

    private void Start() {
        originPos = tr.position;
        originPos.y = 0;
        Destroy(gameObject, 4f);
    }

    private void Update() {
        if(tr.position.y <= 0) {
            tr.position = originPos;
        }
    }
}
