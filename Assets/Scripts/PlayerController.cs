using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");

		Vector3 direction;

		// Makes diagonal movement as fast as moving in a straight line
		if (Mathf.Abs(horizontal) + Mathf.Abs(vertical) > 1) {
			direction = Time.deltaTime * Vector3.Normalize(new Vector3(horizontal, 0, vertical));
		} else {
			direction = new Vector3(horizontal * Time.deltaTime, 0, vertical * Time.deltaTime);
		}

		transform.Translate(moveSpeed * direction);

	}
}
