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
                _uiManager = ContentManager.Instance.UI;

            if (_playerManager == null)
                _playerManager = ContentManager.Instance.Player;

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
