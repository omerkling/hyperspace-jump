using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUpdater : MonoBehaviour {
    public Text scoreTextBox;
    public Spaceship spaceship;

    private string scoreText;

    // Start is called before the first frame update
    void Start() {
        scoreText = scoreTextBox.text;        
    }

    // Update is called once per frame
    void Update() {
        scoreTextBox.text = scoreText + spaceship.jumpCount;
    }
}
