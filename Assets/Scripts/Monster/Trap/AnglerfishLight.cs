using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnglerfishLight : MonoBehaviour {

    [SerializeField] private float _moveSpeed;
    public float MoveSpeed {
        get { return _moveSpeed * Time.deltaTime; }
        set { _moveSpeed = value; }
    }

    private Vector3 yPos;
    private readonly float YMAX = 5.6f;
    private readonly float YMIN = 4.6f;

    private void Start() {

        yPos = transform.position;
        yPos.y = UnityEngine.Random.Range(YMIN, YMAX);
    }

    private void FixedUpdate() {
        if (transform.position.y < YMIN) {
            yPos.y = YMIN - Definition.TILE_SPACING;
        } else if (transform.position.y > YMAX) {
            yPos.y = YMIN + Definition.TILE_SPACING;
        }

        this.transform.position = Vector3.Lerp(transform.position, yPos, MoveSpeed);
    }
}