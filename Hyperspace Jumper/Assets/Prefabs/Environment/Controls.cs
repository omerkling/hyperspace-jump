using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controls : MonoBehaviour {
    public GameObject spaceship;
    public GameObject tunnel;
    public GameObject restartButton;

    bool canRestart;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    internal void ShowRestart() {
        restartButton.SetActive(true);
        canRestart = true;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) {
            Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
            spaceship.GetComponent<Spaceship>().Jump();
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.KeypadEnter)) {
            if (canRestart) {
                Restart();
            }
        }

        float h = Input.GetAxisRaw("Horizontal");
        if (h > 0) {
            tunnel.GetComponent<TunnelMovement>().RotateClockwise();
        } else if (h < 0) {
            tunnel.GetComponent<TunnelMovement>().RotateCounterClockwise();
        }
    }

    public void Restart() {
        print("restart");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
