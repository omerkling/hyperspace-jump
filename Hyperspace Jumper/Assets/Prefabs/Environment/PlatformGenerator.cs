using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformGenerator : MonoBehaviour {
    private GameObject lastPlatform;
    private TunnelMovement tunnelMover;
    public Spaceship spaceship;
    public float spawnPointZ = 92.2f;
    public float minGap = 0.2f;
    public float maxGap = 1f;
    public float gapMargin = 0.15f;
    public float gapAngle = 81.2f;
    public float minSize = 0.5f;
    public float maxSize = 6;
    public float sizeMargin = 0.2f;
    private int platformCount = 0;

    // Start is called before the first frame update
    void Start() {
        tunnelMover = GetComponent<TunnelMovement>();
        lastPlatform = tunnelMover.startPlatform;
    }

    // Update is called once per frame
    void Update() {
        float furthestZ = (lastPlatform.transform.localScale.z / 2f) + lastPlatform.transform.localPosition.z;
        if (furthestZ < spawnPointZ) {

            platformCount++;
            GameObject newPlatform = tunnelMover.GetPlatform();
            float t = Math.Abs((2 * spaceship.jumpPower) / Physics.gravity.y);
            float jumpDistance = t * tunnelMover.SpeedForScore(platformCount - 1);
            // The gap can be less than jumpDistance, but should never be more
            float effectiveMaxGap = jumpDistance * (maxGap - gapMargin);
            float gap = Random.Range(effectiveMaxGap * minGap, effectiveMaxGap);
            // If the player jumps at the very end of the previous platform, they should always be able
            // to land on the next platform
            float effectiveMinPlatformSize = Math.Max(jumpDistance * minSize, (jumpDistance - gap) + (jumpDistance * sizeMargin));
            float effectiveMaxPlatformSize = Math.Max(jumpDistance * maxSize, effectiveMinPlatformSize);
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
