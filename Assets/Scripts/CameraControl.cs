using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

	private Transform tr;

	public Animator titleAnimator;
	public GameObject player;
	public Vector3 offset;
	public float followSpeed;
	public float initHeight = 18f;
	public float isOnTitleTime;
	private bool isOnTitle = false;

	private Vector3 cameraPos;

	private float initCameraSpeed = 0.8f;
	private Vector3 endPos;
	private Vector3 startPos;

    private void Awake() {
		tr = GetComponent<Transform>();

		InitStartPos();
    }

    private void Start() {
		StartCoroutine(CameraMoveAtFirstStart());
    }

	private IEnumerator CameraMoveAtFirstStart() {
		SpawnManager.init.TileUpDownAnimStart(true);
		yield return new WaitForSeconds(0.5f);
		while (tr.localPosition.y > (endPos.y + 0.01f)) {
			tr.localPosition = Vector3.Lerp(tr.localPosition, endPos, Time.deltaTime * initCameraSpeed);

			if(!isOnTitle && tr.position.y < isOnTitleTime) {
				isOnTitle = true;
				titleAnimator.Play("Title");
            }
			yield return null;
		}

		tr.localPosition = endPos;
	}

    public void PosReset() {
		transform.position = new Vector3(offset.x, offset.y, -1.4f);
    }

	private void LateUpdate() {
		if (player == null) return;

		cameraPos = player.transform.position + offset;
		cameraPos.x = offset.x;

		tr.position = Vector3.Lerp(transform.position, cameraPos, followSpeed * Time.deltaTime);
	}

	private void InitStartPos() {
		startPos = tr.localPosition;
		endPos = tr.localPosition;
		startPos.y = initHeight;
		tr.localPosition = startPos;
    }
}