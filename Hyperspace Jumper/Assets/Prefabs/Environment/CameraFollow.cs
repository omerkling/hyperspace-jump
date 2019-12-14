using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    private float distance;
    public float rotationDamping = 0.27f;

    // Use this for initialization
    void Start() {
        distance = transform.position.z - target.position.z;
    }

    void LateUpdate() {
        FollowTarget();
    }

    private void FollowTarget() {
        
        float currentHeight = transform.position.y;
             
        
        transform.position = target.position + Vector3.forward * distance;

        transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);
    }
}
