namespace Assets.Scripts.Generic
{
    using UnityEngine;
    using UI;
    using Player;

    public class PauseManager : MonoBehaviour
    {
        private bool _isPaused;
        private float _currentPauseInputCooldown;
        [SerializeField] private float _pauseInputCooldown = 0.5f;

        private UIManager _uiManager;
        private PlayerManager _playerManager;

        private void Awake()
        {
            ValueManager.Instance.RegisterManager<PauseManager>(this);
        }

        public void Update()
        {
            HandleCooldowns();
        }

        private void HandleCooldowns()
        {
            if (_currentPauseInputCooldown > 0)
            {
                _currentPauseInputCooldown -= Time.deltaTime;
            }
        }

        public bool IsPaused()
        {
            return _isPaused;
        }

        public bool CanPause()
        {
            return _currentPauseInputCooldown <= 0;
        }

        public void TogglePauseGame()
        {
            if (_uiManager == null)
                _uiManager = ValueManager.Instance.UI;

            if (_playerManager == null)
                _playerManager = ValueManager.Instance.Player;

            if (CanPause())
            {
                if (!_isPaused)
                {
                    _playerManager.PauseGame();
                    _uiManager.PauseGame();
                    _isPaused = true;
                    _currentPauseInputCooldown = _pauseInputCooldown;
                }
                else
                {
                    _playerManager.UnpauseGame();
                    _uiManager.UnpauseGame();
                    _isPaused = false;
                    _currentPauseInputCooldown = _pauseInputCooldown;
                }
            }
        }
    }
}
