﻿using UnityEngine;

    void OnEnable() {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable() {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    /// <summary> Whenever a new level is loaded, check for a new camera instance. </summary>
        // Exit conditions
        if (!_isInitialized) { return; }