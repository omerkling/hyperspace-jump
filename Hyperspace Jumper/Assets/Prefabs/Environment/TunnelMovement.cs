using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelMovement : MonoBehaviour
{
    public float rotationSpeed = 45f;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
