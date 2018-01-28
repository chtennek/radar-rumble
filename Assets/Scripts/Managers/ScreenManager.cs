using UnityEngine;using UnityEngine.SceneManagement;public class ScreenManager : MonoBehaviour {
    // Constants
    public const string TitleScreen = "title";    public const string GameScreen = "game-ken";    // Properties    private string currentScreen = TitleScreen;    private float inputCooldown = 1f; // Prevents players from spamming Start button and skipping scenes    // ========================= Initialization =========================    // Use this for initialization    public void Initialize() {}    // Update is called once per frame    void Update() {        // Accept Submit button at the title screen        if (Input.GetButtonUp("Submit") && Time.timeSinceLevelLoad > inputCooldown) {            if (currentScreen == TitleScreen) {
                GoToNextScene();
            }        }    }    // Go to the next screen    public void GoToNextScene() {
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
    }}