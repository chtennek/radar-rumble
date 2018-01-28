using UnityEngine;

public class GameManager : MonoBehaviour {
    // Singleton
    private static GameManager _instance;           // The instance of the GameManager
    private bool _isCreated = false;                // Is the GameManager already created?
    public DataManager dataManager;
    public SoundManager soundManager;

    // =====================================================================================
    // Construction
    // =====================================================================================
    /// <summary> Create singleton if allowed. </summary>
    private void Awake() {
        // Don't have more than one GameManager at a time
        if (!_isCreated && FindObjectsOfType(typeof(GameManager)).Length > 1) {
            Destroy(this.gameObject);
            return;
        }

        // Create GameManager if not already created
        if (!_isCreated) {
            _isCreated = true;
            DontDestroyOnLoad(this);
            _instance = this;
            Setup();
        }
    }

    /// <summary> Initialization </summary>
    private void Setup() {
        // Create all managers
        dataManager = this.GetComponent<DataManager>();
        if (dataManager != null) { dataManager.Initialize(); }
        soundManager = this.GetComponent<SoundManager>();
        if (soundManager != null) { soundManager.Initialize(); }
    }

    // =====================================================================================
    // Destruction
    // =====================================================================================
    /// <summary> Raises the application quit event. </summary>
    private void OnApplicationQuit() {
    }

    // =====================================================================================
    // Accessors
    // =====================================================================================
    public static GameManager GetInstance() { return _instance; }
}
