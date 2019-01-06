using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {

	public GameObject player;
	// How far up from player
	public float height = 2.5f;
	// How far behind player
	public float distance = 7.0f;
	public float mouseSensitivity = 100f;
	private float rotationAngle = -1.5f;
	private float lookOffset = 0f;

	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 inititalPosition = transform.position;

		float mouseX = -1 * Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;
		float mouseY = -1 * Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;
		
		// Rotate (around the player) using mouse
		rotationAngle = rotationAngle + (mouseX);
		Vector3 cameraPositionForRotation = new Vector3(distance * Mathf.Cos(rotationAngle), height, distance * Mathf.Sin(rotationAngle));

		// Stick to player
		Vector3 playerPosition = player.transform.position;
		Vector3 cameraPositionForPlayer = cameraPositionForRotation + playerPosition;

		// Find distance to ground
		RaycastHit hit;
		Ray downRay = new Ray(new Vector3(cameraPositionForPlayer.x, 90, cameraPositionForPlayer.z), Vector3.down);
		if (Physics.Raycast(downRay, out hit)) {
			//Debug.Log("Camera: " + hit.distance);
		} 
			
		// Pick highest camera position so the camera stays aboe ground
		cameraPositionForPlayer.y = Mathf.Max(hit.point.y + 5, cameraPositionForPlayer.y);

		// This enables looking up and down with mouse (with limits)
		lookOffset -= mouseY;
		lookOffset = Mathf.Clamp(lookOffset, -10f, 10f);

		// Set camera position smoothly
		transform.position = Vector3.Lerp(inititalPosition, cameraPositionForPlayer, 20 * Time.deltaTime);

		// Look at target smoothly
		Quaternion targetRotation = Quaternion.LookRotation(new Vector3(playerPosition.x, playerPosition.y + lookOffset, playerPosition.z) - transform.position);
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 20 * Time.deltaTime);


	}

}
