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
    public float minGap = 0.6f;
    public float gapMargin = 4f;
    public float gapAngle = 81.2f;
    public float minSize = 0.0f;
    public float maxSize = 1f;
    public float sizeMargin = 1.5f;
    private int platformCount = 0;

    // Start is called before the first frame update
    void Start() {
        tunnelMover = GetComponent<TunnelMovement>();
        lastPlatform = tunnelMover.startPlatform;

        // Ensure the start platform is always 3 seconds long
        float startPosition = lastPlatform.transform.position.z - lastPlatform.transform.localScale.z / 2f;
        float size = tunnelMover.speed * 3f;
        SizePlatform(lastPlatform, size);
        PositionPlatform(lastPlatform, startPosition, 0);
    }

    // Update is called once per frame
    void Update() {
        float furthestZ = (lastPlatform.transform.localScale.z / 2f) + lastPlatform.transform.localPosition.z;
        if (furthestZ < spawnPointZ) {

            platformCount++;
            GameObject newPlatform = tunnelMover.GetPlatform();
            float t = spaceship.HangTime();
            float platformSpeed = tunnelMover.SpeedForScore(platformCount - 1);
            float jumpDistance = t * platformSpeed;
            // The gap can be less than jumpDistance, but should never be more
            float effectiveMaxGap = jumpDistance - gapMargin;
            float gap = Random.Range(effectiveMaxGap * minGap, effectiveMaxGap);
            // If the player jumps at the very end of the previous platform, they should always be able
            // to land on the next platform
            float effectiveMinPlatformSize = Math.Max((platformSpeed * minSize) + sizeMargin, (jumpDistance - gap) +  sizeMargin);
            float effectiveMaxPlatformSize = Math.Max(platformSpeed * maxSize, effectiveMinPlatformSize);
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
