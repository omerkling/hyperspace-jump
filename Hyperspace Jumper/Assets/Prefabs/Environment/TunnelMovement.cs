using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelMovement : MonoBehaviour
{
    public float rotationSpeed = 45f;
    public float speed = 6.5f;

    // Start is called before the first frame update
    void Start() {
    }
        
    void FixedUpdate() {
        // TODO Switch to an object pool
        foreach (GameObject ground in GameObject.FindGameObjectsWithTag(Tags.GROUND)) {
            ground.transform.position = ground.transform.position + Vector3.back * speed * Time.deltaTime;
        }
    }

    internal void RotateClockwise() {
        print("clockwise");
        transform.Rotate(Vector3.forward * -rotationSpeed * Time.deltaTime);
    }

    internal void RotateCounterClockwise() {
        print("counter clockwise");
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}
