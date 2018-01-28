using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour {
    // Properties
    private int sceneNum = 0;
    private float inputCooldown = 1f; // Prevents players from spamming Start button and skipping scenes

    // ========================= Initialization =========================
    // Use this for initialization
    public void Initialize() {}

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonUp("Submit") && Time.timeSinceLevelLoad > inputCooldown) {
            switch(sceneNum) {
                case 0:
                    SceneManager.LoadScene("game-ken");
                    break;
                case 1:
                    break;
            }
            sceneNum++;
        }
    }
}
