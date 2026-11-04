namespace Assets.Scripts.Generic
{
    using System.Collections.Generic;
    using UnityEngine;
    using UI;
    using Player;

    public class InputManager : MonoBehaviour
    {
        private PlayerManager _playerManager;
        private Player _currentPlayer;
        private CameraManager _cameraManager;
        private PauseManager _pauseManager => ContentManager.Instance.Pause;
        private Vector2 _lookPosition;

        private void Awake()
        {
            ContentManager.Instance.RegisterManager<InputManager>(this);
            _playerManager = ContentManager.Instance.Player;
            _currentPlayer = _playerManager.GetCurrentPlayer();
            _cameraManager = ContentManager.Instance.Camera;
        }

        public void Update()
        {
            HandleCooldowns();

            // Mouse
            var mouseX = Input.GetAxisRaw("Mouse X") * SettingsManager.MOUSE_SENSITIVITY;
            var mouseY = Input.GetAxisRaw("Mouse Y") * SettingsManager.MOUSE_SENSITIVITY;
            _lookPosition = new Vector2(mouseX, mouseY);
        }

        private void HandleCooldowns()
        {
        }
    }
}
