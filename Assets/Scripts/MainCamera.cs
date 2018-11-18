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
		Vector3 cameraPositionForPlayer = new Vector3(playerPosition.x, playerPosition.y + height, playerPosition.z);

		// Rotate around player depending on player's rotation
		Vector3 cameraPositionForRotation = distance * new Vector3(-1 * Mathf.Sin(playerAngle), 0, -1 * Mathf.Cos(playerAngle));

		transform.position = cameraPositionForPlayer + cameraPositionForRotation;
		transform.LookAt(playerPosition);

	}

}
