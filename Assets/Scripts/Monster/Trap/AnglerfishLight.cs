using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnglerfishLight : MonoBehaviour {

    [SerializeField] private float _moveSpeed;
    public float MoveSpeed {
        get { return _moveSpeed * Time.deltaTime; }
        set { _moveSpeed = value; }
    }

    [SerializeField] private float tintFadeTime;

    private Vector3 velocity = Vector3.zero;
    private Vector3 yPos;

    static private Material material;
    static private Color materialTintColor;

    private readonly float YMAX = 2.5f;
    private readonly float YMIN = 1f;
    private readonly float OFFSET = 0.1f;

    private void Start() {
        yPos = transform.position;
        yPos.y = UnityEngine.Random.Range(YMIN, YMAX);
        material = GetComponent<MeshRenderer>().material;
        materialTintColor = material.color;

        StartCoroutine(nameof(MaterialTintFade));
    }

    private void FixedUpdate() {
        if (transform.position.y <= (YMIN + OFFSET)) {
            yPos.y = YMAX;
        } else if (transform.position.y >= (YMAX - OFFSET)) {
            yPos.y = YMIN;
        }
        this.transform.position = Vector3.SmoothDamp(transform.position, yPos, ref velocity, MoveSpeed);
    }

    private IEnumerator MaterialTintFade() {
        float time = 0f;

        while (true) {
            if (materialTintColor.a > 0) {
                time += Time.deltaTime / tintFadeTime;
                materialTintColor.a = Mathf.Lerp(1, 0, time);
                material.color = materialTintColor;
            }
/*            } else if (materialTintColor.a <= 0.1) {
                materialTintColor.a = Mathf.Lerp(0, 1, tintFadeSpeed * Time.deltaTime);
                material.color = materialTintColor;
            }*/
            yield return null;
        }
    }

    private IEnumerator FadeIn() {
        while (materialTintColor.a >= 0) {
            materialTintColor.a = Mathf.Clamp01(materialTintColor.a + tintFadeTime * Time.deltaTime);
            yield return null;
        }
    }
}