using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YetiSnowBall : MonoBehaviour {

    public Transform tr;
    public Vector3 initPos;
    public Vector3 target;
    public float journeyTime = 20.0F;
    private float startTime;
    public float reduceHeight = 25f;

    private bool isStart = false;

    private void OnEnable() {
        tr = this.transform;
        initPos = tr.position;
        target = GameManager.init.player.transform.position;
        target.z -= 0.5f;
        startTime = Time.time;
        isStart = true;
    }
    void Update() {
        if (isStart) {
            Vector3 center = (tr.position + target) * 0.5F;
            center -= new Vector3(0, 1f * reduceHeight, 0);
            Vector3 riseRelCenter = tr.position - center;
            Vector3 setRelCenter = target - center;
            float fracComplete = (Time.time - startTime) / journeyTime;
            tr.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete);
            tr.position += center;

        }

        if (CheckArrived()) {
            tr.position = initPos;
            this.gameObject.SetActive(false);
        }
    }

    private bool CheckArrived() {
        return (tr.position.z - 0.1 < target.z && tr.position.z + 0.1 > target.z);
    }
}