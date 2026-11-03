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
        private PauseManager _pauseManager => ValueManager.Instance.Pause;
        private Vector2 _lookPosition;

        private float _currentScrollInputCooldown;

        private void Awake()
        {
            ValueManager.Instance.RegisterManager<InputManager>(this);
            _playerManager = ValueManager.Instance.Player;
            _currentPlayer = _playerManager.GetCurrentPlayer();
            _cameraManager = ValueManager.Instance.Camera;
        }

        public void Update()
        {
            HandleCooldowns();

            // Mouse
            var mouseX = Input.GetAxisRaw("Mouse X") * SettingsManager.MOUSE_SENSITIVITY;
            var mouseY = Input.GetAxisRaw("Mouse Y") * SettingsManager.MOUSE_SENSITIVITY;
            _lookPosition = new Vector2(mouseX, mouseY);
            _cameraManager.UpdateCameraRotation(_lookPosition);
        }

        private void HandleCooldowns()
        {
        }
    }
}
