﻿using UnityEngine;

public class DataManager : MonoBehaviour {
    private GameObject radar;
    public Vector3 radarRotation;

    // =====================================================================================
    // Construction
    // =====================================================================================
    // Initialization
    public void Initialize() {
        radar = GameObject.Find("Radar");
    }

    // =====================================================================================
    // Update
    // =====================================================================================
    void Update () {
		if (radar != null) {
            radarRotation = radar.transform.eulerAngles;
        }
	}
}