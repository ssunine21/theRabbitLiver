using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnglerfishLight : MonoBehaviour {

    [SerializeField] private float _moveSpeed;
    public float MoveSpeed {
        get { return _moveSpeed * Time.deltaTime; }
        set { _moveSpeed = value; }
    }

    [SerializeField] private float tintFadeSpeed;

    private Vector3 velocity = Vector3.zero;
    private Vector3 yPos;

    private float tintFadeTime;
    static private bool _isFade = false;
    public bool isFade {
        get { return _isFade; }
        set {
            tintFadeTime = 0;
            _isFade = value; 
        }
    }

    private Material material;
    private Color materialTintColor;

    private readonly float YMAX = 2.5f;
    private readonly float YMIN = 1f;
    private readonly float OFFSET = 0.1f;
    private float fadeTime = 0f;

    private void Start() {
        yPos = transform.position;
        yPos.y = UnityEngine.Random.Range(YMIN, YMAX);
        material = GetComponent<MeshRenderer>().material;
        materialTintColor = material.color;

        StartCoroutine(nameof(MaterialTintFade));
    }

    private void FixedUpdate() {
        MoveUpDown();
        MaterialTintFade();
    }

    private void MoveUpDown() {
        if (transform.position.y <= (YMIN + OFFSET)) {
            yPos.y = YMAX;
        } else if (transform.position.y >= (YMAX - OFFSET)) {
            yPos.y = YMIN;
        }
        this.transform.position = Vector3.SmoothDamp(transform.position, yPos, ref velocity, MoveSpeed);
    }

    private void MaterialTintFade() {
        tintFadeTime += Time.deltaTime / tintFadeSpeed;
        if (isFade) {
            if (material.color.a <= 0) return;
            materialTintColor.a = Mathf.Lerp(1, 0, tintFadeTime);
            material.color = materialTintColor;
        } else {
            if (material.color.a >= 1) return;
            materialTintColor.a = Mathf.Lerp(0, 1, tintFadeTime);
            material.color = materialTintColor;
        }
    }
}