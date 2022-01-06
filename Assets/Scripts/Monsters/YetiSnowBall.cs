using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YetiSnowBall : MonoBehaviour {
    private Vector3 startPos;
    private Vector3 targetPos;
    private bool shoting = false;

    public float shotSpeed;

    private void OnEnable() {
        startPos = this.transform.position;
        targetPos = GameManager.init.player.transform.position;
        shoting = true;
    }

    private void FixedUpdate() {
        if (shoting) {
            float distance = startPos.x - targetPos.x;
            float nextX = Mathf.MoveTowards(transform.position.x, targetPos.x, shotSpeed * Time.deltaTime);
            float baseY = Mathf.Lerp(startPos.y, targetPos.y, (nextX - startPos.x) / distance);
            float arc = (nextX = startPos.x) * (nextX - targetPos.x) / (-0.25f * distance * distance);
            Vector3 nextPos = new Vector3(nextX, baseY + arc, transform.position.z);

            this.transform.position = nextPos;
        }
    }

}