﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOrbs : MonoBehaviour {

	void Update () {
        transform.Rotate(Vector3.up * Time.deltaTime * 50);
    }
}
