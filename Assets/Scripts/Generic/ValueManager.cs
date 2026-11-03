namespace Assets.Scripts.Generic
{
    using Player;
    using UnityEngine;
    using UI;

    public class ValueManager : MonoBehaviour
    {
        /// <summary>
        /// Managers
        /// </summary>
        public InputManager Input { get; private set; }
        public PlayerManager Player { get; private set; }
        public CameraManager Camera { get; private set; }
        public UIManager UI { get; private set; }
        public PauseManager Pause { get; private set; }

        /// <summary>
        /// Holders
        /// </summary>
        public Transform PlatformHolder { get; private set; }

        private static ValueManager _instance;

        public static ValueManager Instance
        {
            get
            {
                if (_instance is null)
                    Debug.LogError("ValueManager is null");

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

        public void RegisterManager<T>(Component comp) where T : Component
        {
            switch (comp)
            {
                case (InputManager):
                    Input = comp as InputManager;
                    break;
                case (PlayerManager):
                    Player = comp as PlayerManager;
                    break;
                case (CameraManager):
                    Camera = comp as CameraManager;
                    break;
                case (UIManager):
                    UI = comp as UIManager;
                    break;
                case (PauseManager):
                    Pause = comp as PauseManager;
                    break;
                default:
                    Debug.LogError("Manager was not recognized.");
                    break;
            }
        }
    }
}
