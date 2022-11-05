using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChandelierWarring : MonoBehaviour
{
    public GameObject chandelier;
    public float maxSize;
    public float speed;

    private Transform tr;
    private Transform chandelierTr;
    private Vector3 originScale;
    private Vector3 chandelierPos;
    private float time;

    private void Awake() {
        tr = GetComponent<Transform>();
    }

    private void Start() {
        originScale = transform.localScale;
        StartCoroutine(SizeUp());
    }

    private void Update() {
        if(chandelierTr != null) {
            chandelierPos = chandelierTr.position;
            chandelierPos.y = 0;
            if(chandelierTr.position.y <= 0) {
                chandelierTr.position = chandelierPos;
            }
        }
    }

    private IEnumerator SizeUp() {
        yield return new WaitForSeconds(Random.Range(0f, 3f));

        while(tr.localScale.x < maxSize) {
            tr.localScale = originScale * (time * speed);
            time += Time.deltaTime;

            if(tr.localScale.x >= maxSize) {
                time = 0f;
                tr.localScale = Vector3.zero;
                break;
            }

            yield return null;
        }
        SpawnChandelier();
    }

    private void SpawnChandelier() {
        Vector3 initPos = tr.position;
        initPos.y = 5;
        chandelierTr = Instantiate(chandelier.transform, initPos, chandelier.transform.rotation);

        Destroy(gameObject);
    }
}
