using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoralBullet : MonoBehaviour {

    public float speed = 1f;

    private Vector3 _direction;
    public Vector3 direction {
        get { return _direction; }
        set { _direction = value; }
    }

    private void Start() {
        StartCoroutine(nameof(DestroyTime));
    }

    private void Update() {
        this.transform.Translate(direction * speed * Time.deltaTime);
    }

    private IEnumerator DestroyTime() {
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(Definition.TAG_PLAYER)) {
            Destroy(this.gameObject);
        }
    }
}