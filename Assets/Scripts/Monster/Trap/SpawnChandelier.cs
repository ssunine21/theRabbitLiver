using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnChandelier : MonoBehaviour
{
    public float delayTime;
    public GameObject chandelierWarring;

    private Vector3 warringPos;

    private void Start() {
        warringPos = transform.position;
        warringPos.y = 0.1f;
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn() {
        while (true) {
            if(Random.Range(0, 10) < 1) {
                Instantiate(chandelierWarring, warringPos, chandelierWarring.transform.rotation);
            }
            yield return new WaitForSeconds(delayTime);
        }
    }
}
