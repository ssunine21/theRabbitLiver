using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowball : MonoBehaviour {
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float translateSpeed;

    private void Start() {
        int range = Random.Range(0, 2);
        Vector3 initPos = transform.position;

        initPos.x = range == 0 ? 1.5f : 4.5f;
        transform.position = initPos;
    }

    private void Update() {
        transform.Translate(new Vector3(0, 0, -translateSpeed * Time.deltaTime), Space.World);
        transform.Rotate(new Vector3(-rotateSpeed * Time.deltaTime, 0, 0));
    }
}