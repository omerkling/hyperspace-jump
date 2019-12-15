﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformGenerator : MonoBehaviour {
    private GameObject lastPlatform;
    private TunnelMovement tunnelMover;
    public float spawnPointZ = 500;
    public float minGap = 2;
    public float maxGap = 5;
    public float gapAngle = 45;
    public float minSize = 3;
    public float maxSize = 7;

    // Start is called before the first frame update
    void Start() {
        tunnelMover = GetComponent<TunnelMovement>();
        lastPlatform = tunnelMover.startPlatform;
    }

    // Update is called once per frame
    void Update() {
        float furthestZ = (lastPlatform.transform.localScale.z / 2f) + lastPlatform.transform.localPosition.z;
        if (furthestZ < spawnPointZ) {
            GameObject newPlatform = tunnelMover.GetPlatform();
            SizePlatform(newPlatform, Random.Range(minSize, maxSize));
            PositionPlatform(newPlatform,
                furthestZ + Random.Range(minGap, maxGap),
                Random.Range(-gapAngle, gapAngle));
            lastPlatform = newPlatform;
        }
    }

    private void SizePlatform(GameObject platform, float length) {
        Vector3 scale = platform.transform.localScale;
        scale.z = length;
        platform.transform.localScale = scale;
    }

    private void PositionPlatform(GameObject platform, float startZ, float angle) {
        Vector3 position = lastPlatform.transform.localPosition;
        position.z = startZ + (platform.transform.localScale.z / 2f);
        platform.transform.localPosition = position;
        platform.transform.localRotation = lastPlatform.transform.localRotation;
        platform.transform.RotateAround(gameObject.transform.position, Vector3.forward, angle);
    }
}