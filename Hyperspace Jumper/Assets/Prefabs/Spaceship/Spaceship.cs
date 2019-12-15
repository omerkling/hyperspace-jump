using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    private Rigidbody myBody; 
    public float jumpPower = 4.2f;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start() {        
        myBody = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == Tags.GROUND) {
            isGrounded = true;
        }
    }
    void OnCollisionExit(Collision collision) {
        if (collision.gameObject.tag == Tags.GROUND) {
            isGrounded = false;
        }
    }

    void OnCollisionStay(Collision collision) {
        if (collision.gameObject.tag == Tags.GROUND) {
            isGrounded = true;
        }
    }

    public void Jump() {
        if (isGrounded) {
            myBody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);        
            isGrounded = false;
        }
    }
}
