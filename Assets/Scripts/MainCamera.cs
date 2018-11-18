using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		// Stick to player
		Vector3 playerPosition = player.transform.position;
		transform.position = new Vector3(playerPosition.x, playerPosition.y + 2.5f, playerPosition.z - 7f);
		transform.LookAt(playerPosition);

	}

}
