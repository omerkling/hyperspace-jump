using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelMovement : MonoBehaviour {
    public GameObject platformPrefab;
    public GameObject startPlatform;
    public float rotationSpeed = 45f;
    public float speed = 6.5f;
    public float destroyZ = -30f;

    Queue<GameObject> platformPool = new Queue<GameObject>();
    List<GameObject> activePlatforms = new List<GameObject>();

    // Start is called before the first frame update
    void Start() {
        activePlatforms.Add(startPlatform);
    }
        
    void FixedUpdate() {
        for (int i = activePlatforms.Count - 1; i > -1; i--) {
            GameObject platform = activePlatforms[i];
            platform.transform.position = platform.transform.position + Vector3.back * speed * Time.deltaTime;
            if (platform.transform.position.z < destroyZ) {
                platform.SetActive(false);
                activePlatforms.RemoveAt(i);
                platformPool.Enqueue(platform);
            }
        }
    }

    internal void RotateClockwise() {
        transform.Rotate(Vector3.forward * -rotationSpeed * Time.deltaTime);
    }

    internal void RotateCounterClockwise() {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }

    internal GameObject GetPlatform() {
        GameObject platform = platformPool.Count == 0 ? Instantiate(platformPrefab, gameObject.transform) : platformPool.Dequeue();
        platform.SetActive(true);
        activePlatforms.Add(platform);
        return platform;
    }
}
