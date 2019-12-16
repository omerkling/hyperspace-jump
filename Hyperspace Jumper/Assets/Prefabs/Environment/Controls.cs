using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {
    public GameObject spaceship;
    public GameObject tunnel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            spaceship.GetComponent<Spaceship>().Jump();
        }

        float h = Input.GetAxisRaw("Horizontal");
        if (h > 0) {
            tunnel.GetComponent<TunnelMovement>().RotateClockwise();
        } else if (h < 0) {
            tunnel.GetComponent<TunnelMovement>().RotateCounterClockwise();
        }
    }
}
