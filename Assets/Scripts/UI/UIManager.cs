namespace Assets.Scripts.UI
{
    using Generic;
    using System.Linq;
    using TMPro;
    using UnityEngine;
    using Player;

    public class UIManager : MonoBehaviour
    {
        [Header("PlayerPanel")]
        [SerializeField] private GameObject _interactTextObj;

        [Header("Pause")]
        [SerializeField] private GameObject _pausePanel;

        private PauseManager _pauseManager => ContentManager.Instance.Pause;

        private PlayerManager _playerManager;

        private void Awake()
        {
            ContentManager.Instance.RegisterManager<UIManager>(this);
        }

        private void Update()
        {
            if (_pauseManager.IsPaused())
                return;
        }

        public void PauseGame()
        {
            _pausePanel.SetActive(true);
            EnableCursor();
        }

        public void UnpauseGame()
        {
            _pausePanel.SetActive(false);
            DisableCursor();
        }

        public void EnableCursor()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        public void DisableCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}