using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

	public GameObject player;
	public Vector3 offset;
	public float followSpeed;

	private Vector3 cameraPos;

	private void LateUpdate() {
		cameraPos = player.transform.position + offset;
		cameraPos.x = offset.x;

		transform.position = Vector3.Lerp(transform.position, cameraPos, followSpeed * Time.deltaTime);
	}
}