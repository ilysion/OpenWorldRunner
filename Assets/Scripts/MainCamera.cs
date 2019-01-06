using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {

	public GameObject player;
	// How far up from player
	public float height = 2.5f;
	// How far behind player
	public float distance = 7.0f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
		// Get player's current rotation
		float playerAngle = Mathf.Deg2Rad * player.transform.rotation.eulerAngles.y;

		// Stick to player
		Vector3 playerPosition = player.transform.position;
		Vector3 cameraPositionForPlayer = new Vector3(playerPosition.x, playerPosition.y + height, playerPosition.z - distance);

		// Find distance to ground
		RaycastHit hit;
		Ray downRay = new Ray(cameraPositionForPlayer, Vector3.down);
		if (Physics.Raycast(downRay, out hit)) {
			Debug.Log("Camera: " + hit.distance);
			// ???
		} else {
			// If camera would appear under terrain cast ray upwards
			Ray upRay = new Ray(cameraPositionForPlayer, Vector3.up);
			if (Physics.Raycast(upRay, out hit)) {
				Debug.Log("Camera Below: " + hit.distance);
				// ???
			}
		}
			
		// Make camera stay above ground
		if (hit.distance < 5) {
			cameraPositionForPlayer.y = hit.point.y + 5;
		}

		// Rotate around player depending on player's rotation
		//Vector3 cameraPositionForRotation = distance * new Vector3(-1 * Mathf.Sin(playerAngle), 0, -1 * Mathf.Cos(playerAngle));

		//Vector3 cameraPositionForRotation = distance * new Vector3(-1, 0, -1);
		
		transform.position = cameraPositionForPlayer; // + cameraPositionForRotation;
		transform.LookAt(playerPosition);

	}

}
