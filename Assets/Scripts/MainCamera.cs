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

	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {

		float mouseX = -1 * Input.GetAxis("Mouse X") * Time.deltaTime;
		
		// Rotate (around the player) using mouse
		rotationAngle = rotationAngle + (mouseX * mouseSensitivity);
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
		
		transform.position = cameraPositionForPlayer;
		transform.LookAt(playerPosition);

	}

}
