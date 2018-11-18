using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	public float jumpHeight;
	public Terrain terrain;
	// Distance from center of model to bottom
	private float playerHeightOffset = 1;
	//private Rigidbody rb; 
	private bool jumping = false;
	private float gravity = 0.3f;
	private float jumpAcceleration;
	public float mouseSensitivity = 100;

	// Use this for initialization
	void Start () {
		//rb = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
		Cursor.lockState = CursorLockMode.Locked;

		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");
		float jump = Input.GetAxis("Jump");
		float mouseX = Input.GetAxis("Mouse X");
		

		transform.Rotate(mouseSensitivity * mouseX * Vector3.up * Time.deltaTime);



		if (jump > 0 && !jumping) {
			jumpAcceleration = jumpHeight;
			jumping = true;
		}

 		Vector3 direction;

 		// Makes diagonal movement as fast as moving in a straight line
		if (Mathf.Abs(horizontal) + Mathf.Abs(vertical) > 1) {
			direction = Time.deltaTime * Vector3.Normalize(new Vector3(horizontal, 0, vertical));
		} else {
			direction = Time.deltaTime * new Vector3(horizontal, 0, vertical);
		}

		// Distance between terrain and player 
		float deltaHeight = terrain.SampleHeight(transform.position) - transform.position.y + playerHeightOffset;

		// If jump value is higher than terrain height use jump value
		transform.Translate(moveSpeed * direction.x, Mathf.Max(jumpAcceleration, deltaHeight), moveSpeed * direction.z);
		jumpAcceleration -= Time.deltaTime * gravity;

		// Reset jump if landed
		if (jumpAcceleration < deltaHeight) {
			jumping = false;
		}
 	}
}
