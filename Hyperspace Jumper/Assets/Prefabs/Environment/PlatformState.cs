using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformState : MonoBehaviour {
    public bool canScore;

    internal void Reset() {
        canScore = true;
    }
}
