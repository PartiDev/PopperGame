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
