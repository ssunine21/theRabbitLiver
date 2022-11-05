using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObject : MonoBehaviour {
    public float speed;
    public float maxHeight = 2f;
    public float delayTime;

    private float height;
    private Vector3 initPos;

    private bool isUpDownAnim = false;
    private int direction;

    private void FixedUpdate() {
        if (isUpDownAnim) {
            if (this.transform.position.y > height) direction = -1;
            this.transform.Translate(Vector3.up * speed * Time.deltaTime * direction);

            if (initPos.y > this.transform.position.y) {
                this.transform.position = initPos;
                isUpDownAnim = false;
            }
        }
    }

    public void StartUpDownCoroutine() {
        StartCoroutine(nameof(UpDownDelay));
    }

    IEnumerator UpDownDelay() {
        direction = 1;
        initPos = this.transform.position;
        height = Random.Range(0.5f, maxHeight);

        float delay = this.transform.position.z * delayTime;
        yield return new WaitForSeconds(delay);

       //SoundManager.init.PlaySFXSound(Definition.SoundType.Tik);

        isUpDownAnim = true;
    }
}