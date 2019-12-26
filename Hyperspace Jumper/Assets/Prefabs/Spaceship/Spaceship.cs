using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour {
    public GameObject explosionPrefab;
    private Rigidbody myBody; 
    public float jumpPower = 4.2f;
    private bool isGrounded;
    private bool nearGround;
    private bool jumpWhenGrounded;

    public int jumpCount { get; private set; }


    // Start is called before the first frame update
    void Start() {        
        myBody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == Tags.GROUND) {
            nearGround = true;
        }
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == Tags.GROUND) {
            isGrounded = true;
            PlatformState platformState = collision.gameObject.GetComponent<PlatformState>();
            if (platformState != null && platformState.canScore) {
                jumpCount++;
                platformState.canScore = false;
            }
            if (jumpWhenGrounded) {
                Jump();
            }
        }
    }
    void OnCollisionExit(Collision collision) {
        if (collision.gameObject.tag == Tags.GROUND) {
            isGrounded = false;
            nearGround = false;
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
            nearGround = false;
            jumpWhenGrounded = false;
        } else if (nearGround) {
            jumpWhenGrounded = true;
        }
    }

    public void Explode() {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
}
