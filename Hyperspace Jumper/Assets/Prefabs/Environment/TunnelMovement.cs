using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelMovement : MonoBehaviour {
    public GameObject platformPrefab;
    public GameObject startPlatform;
    public Spaceship spaceship;
    public float rotationSpeed = 45f;
    public float speed = 6.5f;
    public float destroyZ = -30f;
    public float speedIncrement = 1.5f;
    public float platformsPerLevel = 10f;

    Queue<GameObject> platformPool = new Queue<GameObject>();
    List<GameObject> activePlatforms = new List<GameObject>();

    // Start is called before the first frame update
    void Start() {
        activePlatforms.Add(startPlatform);
    }
        
    void FixedUpdate() {
        float moveSpeed = SpeedForScore(spaceship.jumpCount);
        print("Speed " + moveSpeed);
        for (int i = activePlatforms.Count - 1; i > -1; i--) {
            GameObject platform = activePlatforms[i];
            platform.transform.position = platform.transform.position + Vector3.back * moveSpeed * Time.deltaTime;
            if ((platform.transform.position.z + platform.transform.localScale.z) < destroyZ) {
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
        platform.GetComponent<PlatformState>().Reset();
        activePlatforms.Add(platform);
        return platform;
    }

    public float SpeedForScore(int score) {
        return (float)(speed + (Math.Floor(score / platformsPerLevel) * speedIncrement));
    }
}
