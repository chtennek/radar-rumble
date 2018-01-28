﻿using UnityEngine;
    // Constants
    public const string TitleScreen = "title";
                GoToNextScene();
            }
        switch (currentScreen) {
            case TitleScreen:
                currentScreen = GameScreen;
                SceneManager.LoadScene(currentScreen);
                break;

            case GameScreen:
                currentScreen = TitleScreen;
                SceneManager.LoadScene(currentScreen);
                break;
        }
    }