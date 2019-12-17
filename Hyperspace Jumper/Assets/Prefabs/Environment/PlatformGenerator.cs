using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformGenerator : MonoBehaviour {
    private GameObject lastPlatform;
    private TunnelMovement tunnelMover;
    public Spaceship spaceship;
    public float spawnPointZ = 500;
    public float minGap = 2;
    public float maxGap = 5;
    public float gapMargin = 0.01f;
    public float gapAngle = 45;
    public float minSize = 3;
    public float maxSize = 7;
    public float sizeMargin = 0.2f;    

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

            float t = Math.Abs((2 * spaceship.jumpPower) / Physics.gravity.y);
            float jumpDistance = t * tunnelMover.speed;
            // The gap can be less than jumpDistance, but should never be more
            float gap = Random.Range(Math.Min(minGap, jumpDistance), Math.Min(jumpDistance - gapMargin, maxGap));
            // If the player jumps at the very end of the previous platform, they should always be able
            // to land on the next platform
            float effectiveMinPlatformSize = Math.Max(minSize, jumpDistance - gap + sizeMargin);
            float effectiveMaxPlatformSize = Math.Max(maxSize, effectiveMinPlatformSize);
            float size = Random.Range(effectiveMinPlatformSize, effectiveMaxPlatformSize);

            // Prevent platforms from being placed furthur around then tunnel than is reachable.
            float effectiveMaxAngle = Math.Min(gapAngle, t * tunnelMover.rotationSpeed);

            SizePlatform(newPlatform, size);
            PositionPlatform(newPlatform,
                furthestZ + gap,
                Random.Range(-effectiveMaxAngle, effectiveMaxAngle));
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
