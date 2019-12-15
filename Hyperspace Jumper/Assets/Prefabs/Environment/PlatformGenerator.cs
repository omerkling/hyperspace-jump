﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour {
    public GameObject platformPrefab;
    public GameObject startingPlatform;
    private GameObject lastPlatform;
    private float tunnelRadius;
    public float spawnPointZ = 65;
    public float gapSize = 3;
    public float defaultSize = 5;

    // Start is called before the first frame update
    void Start() {
        tunnelRadius = startingPlatform.transform.localPosition.y;
        lastPlatform = startingPlatform;
    }

    // Update is called once per frame
    void Update() {
        float furthestZ = (lastPlatform.transform.localScale.z / 2f) + lastPlatform.transform.localPosition.z;
        if (furthestZ < spawnPointZ) {
            GameObject newPlatform = Instantiate(platformPrefab, gameObject.transform);
            SizePlatform(newPlatform, defaultSize);
            PositionPlatform(newPlatform, furthestZ + gapSize);
            lastPlatform = newPlatform;
        }
    }

    private void SizePlatform(GameObject platform, float length) {
        Vector3 scale = platform.transform.localScale;
        scale.z = length;
        platform.transform.localScale = scale;
    }

    private void PositionPlatform(GameObject platform, float startZ) {
        Vector3 position = lastPlatform.transform.localPosition;
        position.z = startZ + (platform.transform.localScale.z / 2f);
        platform.transform.localPosition = position;
    }
}
