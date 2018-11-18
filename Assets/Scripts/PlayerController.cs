using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	public float jumpHeight;
	public Terrain terrain;
	// Distance from center of model to bottom
	private float playerHeightOffset = 1;
	Rigidbody rb; 
	bool jumping = false;

	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");
		float jump = Input.GetAxis("Jump");

		if (jump > 0 && !jumping) {

			rb.isKinematic = false;
			rb.AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);
			jumping = true;
		}

 		Vector3 direction;

 		// Makes diagonal movement as fast as moving in a straight line
		if (Mathf.Abs(horizontal) + Mathf.Abs(vertical) > 1) {
			direction = Time.deltaTime * Vector3.Normalize(new Vector3(horizontal, 0, vertical));
		} else {
			direction = Time.deltaTime * new Vector3(horizontal, 0, vertical);
		}

		float deltaHeight = terrain.SampleHeight(transform.position) - transform.position.y + playerHeightOffset;
		Debug.Log(deltaHeight);

		if (!jumping) {
 			transform.Translate(moveSpeed * direction.x, deltaHeight, moveSpeed * direction.z);
		} else {
			// Moving while jumping can be configured here
			transform.Translate(moveSpeed * direction.x, 0, moveSpeed * direction.z);

			//if (deltaHeight < 0.5) {
			//	rb.isKinematic = true;
			//	jumping = false;
			//}
		}
 	}
}
