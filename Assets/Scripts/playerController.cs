using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

  private float moveSpeed = 8f;
  private float altitudeSpeed = 8f;
  private Vector3 moveDirection;
  private Rigidbody rb;

	// Use this for initialization
	void Awake() {
    rb = GetComponent<Rigidbody>();
  }

	// Update is called once per frame
	void Update () {

    // Gets directional movement from the user
    float horizontalMovement = Input.GetAxisRaw("Horizontal");
    float verticalMovement = Input.GetAxisRaw("Vertical");

    moveDirection = ((horizontalMovement * transform.right) + (verticalMovement * transform.forward)).normalized;
	}

	// FixedUpdate is called once per physics step
	void FixedUpdate () {
    Move();
	}

  void Move() {
    rb.AddForce(moveDirection * moveSpeed);

    if(Input.GetKey("space")) {
      rb.AddForce(transform.up * altitudeSpeed);
    }
    if(Input.GetKey(KeyCode.LeftShift)) {
      rb.AddForce(-transform.up * altitudeSpeed);
    }
  }

}
