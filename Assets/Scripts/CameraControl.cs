using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

	private Transform tr;

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
	private bool isCameraMoveAtFirstStarting = false;


	private void Awake() {
		tr = GetComponent<Transform>();
		InitStartPos();
	}

	public IEnumerator CameraMoveAtFirstStart() {

		SpawnManager.init.TileUpDownAnimStart(true);
		while (true) { //tr.localPosition.y < (endPos.y - 0.05f) && isCameraMoveAtFirstStarting) {

			if (!isCameraMoveAtFirstStarting) {
				yield return null;
				continue;
			}

			tr.localPosition = Vector3.Lerp(tr.localPosition, endPos, Time.deltaTime * initCameraSpeed);

			if(!isOnTitle && tr.position.y > isOnTitleTime) {
				isOnTitle = true;
				UIManager.init.Title.GetComponent<Title>().Play();
            }
			yield return null;
		}

		tr.localPosition = endPos;
		isCameraMoveAtFirstStarting = false;
	}

    public void PosReset() {
		transform.position = new Vector3(offset.x, offset.y, -1.4f);
    }

	private void LateUpdate() {
		if (player == null) {
			isCameraMoveAtFirstStarting = true;
			return;
		}

		isCameraMoveAtFirstStarting = false;

		cameraPos = player.transform.position + offset;
		cameraPos.x = offset.x;

		tr.position = Vector3.Lerp(tr.position, cameraPos, followSpeed * Time.deltaTime);
	}

	private void InitStartPos() {
		startPos = tr.localPosition;
		endPos = tr.localPosition;
		endPos.y = initHeight;
		tr.localPosition = startPos;
    }
}