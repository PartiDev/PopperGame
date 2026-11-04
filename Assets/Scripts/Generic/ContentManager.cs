namespace Assets.Scripts.Generic
{
    using UnityEngine;
    using UI;
    using Player;

    public class ContentManager : MonoBehaviour
    {
        private static ContentManager _instance;

        public static ContentManager Instance
        {
            get
            {
                if (_instance == null)
                    Debug.LogError("ContentManager is null");

                return _instance;
            }
        }

        /// <summary>
        /// Managers
        /// </summary>
        public InputManager Input { get; private set; }
        public PlayerManager Player { get; private set; }
        public CameraManager Camera { get; private set; }
        public UIManager UI { get; private set; }
        public PauseManager Pause { get; private set; }

        public void RegisterManager<T>(T comp) where T : Component
        {
            switch (comp)
            {
                case InputManager input:
                    Input = input;
                    break;

                case PlayerManager player:
                    Player = player;
                    break;

                case CameraManager camera:
                    Camera = camera;
                    break;

                case UIManager ui:
                    UI = ui;
                    break;

                case PauseManager pause:
                    Pause = pause;
                    break;

                case null:
                    Debug.LogError("Tried to register a null manager.");
                    break;

                default:
                    Debug.LogError($"Manager {comp.GetType().Name} was not recognized.");
                    break;
            }
        }


        public Prefabs prefabs;

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

    }

    [System.Serializable]
    public class Prefabs
    {
        public ManagerPrefabs managerPrefabs;
        public UIPrefabs uiPrefabs;
    }

    [System.Serializable]
    public class ManagerPrefabs
    {
        public InputManager inputManager_P;
        public PlayerManager playerManager_P;
        public CameraManager cameraManager_P;
        public UIManager uiManager_P;
        public PauseManager pauseManager_P;
    }

    [System.Serializable]
    public class UIPrefabs
    {
    }
}
