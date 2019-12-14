using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public float distance = 3f;
    public float height = 1f;
    public float rotationDamping = 0.27f;

    // Use this for initialization
    void Start() {
    }

    void LateUpdate() {
        FollowTarget();
    }

    private void FollowTarget() {
        float wantedRotation = target.eulerAngles.y;

        float currentRotation = transform.eulerAngles.y;
        float currentHeight = transform.position.y;

        currentRotation = Mathf.LerpAngle(currentRotation, wantedRotation, rotationDamping * Time.deltaTime);        

        Quaternion rotation = Quaternion.Euler(0f, currentRotation, 0f);
        transform.position = target.position - rotation * Vector3.forward * distance;

        transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);
    }
}
