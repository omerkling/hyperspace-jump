using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCollider : MonoBehaviour {

    public Controls controls;

    private void OnCollisionEnter(Collision collision) {        
        collision.gameObject.GetComponent<Spaceship>().Explode();
        controls.ShowRestart();
    }
}
