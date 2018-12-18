using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

  private float flySpeed = 8f;          // Flight movement speed
  private float altitudeSpeed = 8f;     // Flight rising/lowering speed

  private float walkSpeed = 15f;
  private float maxWalkSpeed = 20f;

  private float gravityForce = 25f;
  private float jumpForce = 500f;

  private bool isJumping = false;
  private bool gravityOn = false;

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

    if(gravityOn) {
      GravityMovement();
    } else {
      NoGravityMovement();
    }

	}

  void NoGravityMovement() {
    rb.AddForce(moveDirection * flySpeed);

    // Fly up
    if(Input.GetKey("space")) {
      rb.AddForce(transform.up * altitudeSpeed);
    }

    // Fly down
    if(Input.GetKey(KeyCode.LeftShift)) {
      rb.AddForce(-transform.up * altitudeSpeed);
    }
  }

  void GravityMovement() {
    rb.AddForce(-Vector3.up * gravityForce);

    rb.AddForce(moveDirection * walkSpeed);
    rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxWalkSpeed);

    // Jump
    if(Input.GetKeyDown("space")) {
      Debug.Log(isJumping);

      if(isJumping == false) {
        isJumping = true;
        rb.AddForce(Vector3.up * jumpForce);
      }
    }
  }

  void OnCollisionEnter(Collision other) {
    if(other.contacts.Length > 0) {
      ContactPoint contact = other.contacts[0];
      if(Vector3.Dot(contact.normal, Vector3.up) > 0.5) {
        isJumping = false;
      }
    }
  }

  void OnTriggerStay(Collider other) {
    if(other.tag == "Gravity Field") {
      if(gravityOn == false) {
        gravityOn = true;
      }
    }
  }

  void OnTriggerExit(Collider other) {
    if(other.tag == "Gravity Field") {
      if(gravityOn == true) {
        gravityOn = false;
      }
    }
  }

}
