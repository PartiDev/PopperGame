namespace Assets.Scripts.Player
{
    using Generic;
    using UnityEngine;

    public class Player : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Rigidbody _rb;

        [Header("Movement")]
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _groundedDist;
        private Vector2 _movementPos;
        private Vector3 _camForward;
        private Vector3 _camRight;

        private PlayerState _playerState = PlayerState.Active;

        private PauseManager _pauseManager => ContentManager.Instance.Pause;

        public PlayerState GetPlayerState()
        {
            return _playerState;
        }

        public void SetPlayerState(PlayerState newState)
        {
            _playerState = newState;
        }

        public Vector3 GetPosition()
        {
            return _rb.transform.position;
        }

        public Rigidbody GetRb()
        {
            return _rb;
        }

        private bool CanMove()
        {
            return _playerState == PlayerState.Active;
        }

        private void Update()
        {
            if (_pauseManager.IsPaused())
                return;

            if (!CanMove())
                return;

            HandleCooldowns();

        }

        private void FixedUpdate()
        {
            if (_pauseManager.IsPaused())
                return;

            if (!CanMove())
                return;

            // RB APPLICATION
            RegularMovement();
        }

        private void RegularMovement()
        {
            Vector3 move = (_rb.transform.forward * _movementPos.y + _rb.transform.right * _movementPos.x);
            Vector3 newVelocity = new Vector3(move.x, _rb.linearVelocity.y, move.z);
            _rb.linearVelocity = newVelocity;
        }

        public void UpdateRotation(float moveRotation, Transform followingCamera)
        {
            _camForward = followingCamera.forward;
            _camRight = followingCamera.right;

            Vector3 rot = Vector3.zero;
            rot.y = moveRotation;
            Quaternion targetRot = Quaternion.Euler(rot);
            _rb.transform.localRotation = targetRot;
        }

        private void HandleCooldowns()
        {
        }

        public void OnDeath()
        {
            this._playerState = PlayerState.GameOver;
        }

        public void OnPause()
        {
            this._rb.isKinematic = true;
        }

        public void OnUnpause()
        {
            this._rb.isKinematic = false;
        }
    }
}

public enum PlayerState { Active, GameOver };