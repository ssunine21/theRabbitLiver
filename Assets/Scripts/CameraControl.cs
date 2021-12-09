using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

	public GameObject player;
	public Vector3 offset;
	public float followSpeed;

	private Vector3 cameraPos;

	public void PosReset() {
		transform.position = new Vector3(offset.x, offset.y, -1.4f);
    }

	private void LateUpdate() {
		if (player == null) return;

		cameraPos = player.transform.position + offset;
		cameraPos.x = offset.x;

		transform.position = Vector3.Lerp(transform.position, cameraPos, followSpeed * Time.deltaTime);
	}
}