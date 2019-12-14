using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    private Rigidbody myBody; 
    private Vector3 speed;
    public float zSpeed = 6.5f;
    public float jumpPower = 4.2f;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start() {
        speed = new Vector3(0f, 0f, zSpeed);
        myBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        
    }

    void FixedUpdate() {
        Move();
    }

    private void Move() {
        myBody.MovePosition(myBody.position + speed * Time.deltaTime);
    }

    private void OnCollisionStay(Collision collision) {
        if (collision.gameObject.tag == Tags.GROUND) {
            isGrounded = true;
        } else {
            isGrounded = false;
        }
    }

    public void Jump() {
        if (isGrounded) {
            print("jumping");
            // TODO: handle rotated space
            myBody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);        
            isGrounded = false;
        }
    }
}
