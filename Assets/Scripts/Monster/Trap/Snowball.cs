using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowball : MonoBehaviour {
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float translateSpeed;

    private void Update() {
        transform.Translate(new Vector3(0, 0, -translateSpeed * Time.deltaTime), Space.World);
        transform.Rotate(new Vector3(-rotateSpeed * Time.deltaTime, 0, 0));
    }
}