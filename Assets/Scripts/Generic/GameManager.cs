namespace Assets.Scripts.Generic
{
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using Player;

    public enum CurrentScene { Menu, MainGame }

    public class GameManager : MonoBehaviour
    {
        [SerializeField] private CurrentScene _currentScene;

        private static GameManager _instance;

        public static GameManager Instance
        {
            get
            {
                if (_instance is null)
                    Debug.LogError("GameManager is null");

                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
                _instance = this;
            }
        }

        private void Start()
        {
            Application.targetFrameRate = 60;
            QualitySettings.vSyncCount = 1;
            Time.timeScale = 1f;

            StartGame();
        }

        private void StartGame()
        {
            Debug.Log("Starting game...");

            RemoveSceneCamera();
            ConfigureSettings();
            InstantiateManagers();

            Debug.Log("Game started");
        }

        private void RemoveSceneCamera()
        {
            var existingCamera = FindAnyObjectByType<Camera>();
            if (!existingCamera || existingCamera.GetComponent<CameraManager>() != null) return;

            Destroy(existingCamera.gameObject);
        }

        private void ConfigureSettings()
        {
        }

        private void InstantiateManagers()
        {
            // Order is important !
            Instantiate(ContentManager.Instance.prefabs.managerPrefabs.pauseManager_P);
            Instantiate(ContentManager.Instance.prefabs.managerPrefabs.uiManager_P);
            Instantiate(ContentManager.Instance.prefabs.managerPrefabs.playerManager_P);
            Instantiate(ContentManager.Instance.prefabs.managerPrefabs.cameraManager_P);
            Instantiate(ContentManager.Instance.prefabs.managerPrefabs.inputManager_P);
        }

        public void RestartScene(int? scene = null)
        {
            var sceneToLoad = scene ?? 0;
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}