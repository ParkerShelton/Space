using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseLook : MonoBehaviour {

  public Camera playerCam;

  private float lookSpeed = 5f;
  private Vector2 rotation = Vector2.zero;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
      Look();
    }

    void Look() {
        rotation.y += Input.GetAxis("Mouse X");
        rotation.x += -Input.GetAxis("Mouse Y");
        // rotation.x = Mathf.Clamp(rotation.x, -15f, 15f);

        transform.eulerAngles = new Vector2(0,rotation.y) * lookSpeed;

        playerCam.transform.localRotation = Quaternion.Euler(rotation.x * lookSpeed, 0, 0);
    }

}
