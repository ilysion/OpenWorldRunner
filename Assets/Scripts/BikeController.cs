using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeController : MonoBehaviour {

	public float acceleration = 400f;
	public float turning = 200f; 
	public float dragOnGround = 2f;
	private Rigidbody rb; 

	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody>();
		//Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {

		float horizontal = Input.GetAxis("Horizontal"); //TODO Time.Deltatime
		float vertical = Input.GetAxis("Vertical");
		
		// Distance to terrain below bike 
		RaycastHit hit;
		Ray downRay = new Ray(transform.position, Vector3.down);
		if (Physics.Raycast(downRay, out hit)) {
			Debug.Log(hit.distance);
			// ???
		}


		// Bike on ground - enable controlling the bike
		if (hit.distance <= 0.5) {
			
			rb.drag = dragOnGround;

			// Acceleration 
			rb.AddForce(transform.forward * vertical * acceleration);

			// Turning 
			rb.AddTorque(transform.up * horizontal * turning);

		// Bike in air
		} else {

			rb.drag = 0.1f;

		}

		// Keeps the bike upright
		if (horizontal == 0 || hit.distance > 0.5) {
			rb.AddTorque(transform.forward * (transform.rotation.z * -50));
		}

		//TODO do something when the bike falls over (rotate back up)

 	}

}
