using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCollider : MonoBehaviour {

    private void OnCollisionEnter(Collision collision) {
        // TODO: Trigger explosion and reset or game over
        Time.timeScale = 0f;
    }
}
