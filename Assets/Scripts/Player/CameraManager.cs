namespace Assets.Scripts.Player
{
    using Assets.Scripts.Generic;
    using UnityEngine;
    using static CONSTANTS;

    public class CameraManager : MonoBehaviour
    {
        [Header("Ref")]
        [SerializeField] private Transform _cameraHolder;
        [SerializeField] private Transform _cameraPivot;
        [SerializeField] private Camera _mainCamera;

        [Header("Setting")]
        [SerializeField] private float _xBaseOffset;
        [SerializeField] private float _yBaseOffset;
        [SerializeField] private float _zBaseOffset;
        [SerializeField] private CameraSetting _cameraSetting;
        [SerializeField] private float _cameraFollowDelay;
        [SerializeField] private float _cameraMaxSpeed;
        [SerializeField] private float _minPivotAngle = -35f;
        [SerializeField] private float _maxPivotAngle = 35f;

        [Header("Cap")]
        [SerializeField] private float _xCappedOffset;
        [SerializeField] private float _zCappedOffset;
        [SerializeField] private float _cappedOffsetEase;

        [Header("Shake")]
        [SerializeField] private float _lightShakeDuration;
        [SerializeField] private float _currentLightShakeDuration;
        [SerializeField] private float _lightShakeAmount;
        [SerializeField] private float _decreaseFactor;

        private Player _playerInst;
        private Transform _playerFollow;
        private Vector3 _originalPos;
        private float _currentShakeAmount;
        private Vector2 _lookPos;
        private Vector3 _cameraFollowVelocity = Vector3.zero;
        private float _lookAngle;
        private float _pivotAngle;
        private bool _followingPlayer = true;

        private PauseManager _pauseManager => ValueManager.Instance.Pause;
        private PlayerManager _playerManager;

        public Camera GetCamera()
        {
            return _mainCamera;
        }

        private void Awake()
        {
            ValueManager.Instance.RegisterManager<CameraManager>(this);
            _originalPos = _cameraHolder.transform.localPosition;
            _playerManager = ValueManager.Instance.Player;
            _playerInst = _playerManager.GetCurrentPlayer();
            _playerFollow = _playerInst.GetRb().transform;

            SetUpCursor();
            SetUpCameraPos();
        }

        private void SetUpCameraPos()
        {
            if (_followingPlayer)
            {
                Vector3 followOffset = new Vector3(_xBaseOffset, _yBaseOffset, _zBaseOffset);
            }
        }

        private void SetUpCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            if (_pauseManager.IsPaused())
                return;

            CameraDetectShake();
        }

        private void LateUpdate()
        {
            if (_pauseManager.IsPaused())
                return;

            var playerState = _playerInst.GetPlayerState();

            if (playerState == PlayerState.Interacting || playerState == PlayerState.GameOver)
                return;

            if (_followingPlayer)
            {
                Vector3 followOffset = new Vector3(_xBaseOffset, _yBaseOffset, _zBaseOffset);
                Vector3 rotatedOffset = _playerFollow.rotation * followOffset;
                Vector3 targetPos = _playerFollow.position + rotatedOffset;
                _cameraHolder.position = targetPos;
            }
        }

        public void UpdateCameraRotation(Vector2 lookPos)
        {
            if (_pauseManager.IsPaused())
                return;

            var playerState = _playerInst.GetPlayerState();

            if (playerState == PlayerState.Interacting || playerState == PlayerState.GameOver)
                return;

            _lookPos = lookPos;

            _lookAngle = _lookAngle + _lookPos.x;
            _pivotAngle = _pivotAngle - _lookPos.y;
            _pivotAngle = Mathf.Clamp(_pivotAngle, _minPivotAngle, _maxPivotAngle);

            Vector3 rot = Vector3.zero;
            rot.y = _lookAngle;
            Quaternion targetRot = Quaternion.Euler(rot);
            _cameraHolder.rotation = targetRot;

            rot = Vector3.zero;
            rot.x = _pivotAngle;
            targetRot = Quaternion.Euler(rot);
            _cameraPivot.localRotation = targetRot;

            if (_followingPlayer)
            {
                _playerInst.UpdateRotation(_lookAngle, _cameraHolder.transform);
            }
        }

        public void FollowPlayer()
        {
            Vector3 followOffset = new Vector3(_xBaseOffset, _yBaseOffset, _zBaseOffset);
            Vector3 rotatedOffset = _playerFollow.rotation * followOffset;
            Vector3 targetPos = _playerFollow.position + rotatedOffset;
            Vector3 playerPos = Vector3.SmoothDamp(_cameraHolder.transform.position, targetPos, ref _cameraFollowVelocity, _cameraFollowDelay, _cameraMaxSpeed, Time.deltaTime);
            _cameraHolder.position = playerPos;
        }

        /// <summary>
        /// Camera Shake
        /// </summary>
        private void CameraDetectShake()
        {
            if (_currentLightShakeDuration <= 0) return;

            _cameraHolder.localPosition = _originalPos + Random.insideUnitSphere * _currentShakeAmount;
            _currentLightShakeDuration -= Time.deltaTime * _decreaseFactor;
        }

        public void CameraShakeLight(float? amount = null)
        {
            _currentLightShakeDuration = _lightShakeDuration;
            _currentShakeAmount = amount != null ? amount.Value : _lightShakeAmount;
        }
    }
}
