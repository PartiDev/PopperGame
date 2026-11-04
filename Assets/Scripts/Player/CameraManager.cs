namespace Assets.Scripts.Player
{
    using Assets.Scripts.Generic;
    using UnityEngine;
    using static CONSTANTS;

    public class CameraManager : MonoBehaviour
    {
        [Header("Ref")]
        [SerializeField] private Transform _anchor;
        [SerializeField] private Transform _pivot;
        [SerializeField] private Transform _effects;
        [SerializeField] private Camera _mainCamera;

        [Header("Setting")]
        [SerializeField] private float _xBaseOffset;
        [SerializeField] private float _yBaseOffset;
        [SerializeField] private float _zBaseOffset;
        [SerializeField] private float _cameraFollowDelay;
        [SerializeField] private float _cameraMaxSpeed;
        [SerializeField] private CameraSetting _cameraSetting;

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

        private PauseManager _pauseManager => ContentManager.Instance.Pause;
        private PlayerManager _playerManager;

        public Camera GetCamera()
        {
            return _mainCamera;
        }

        private void Awake()
        {
            ContentManager.Instance.RegisterManager<CameraManager>(this);
            _originalPos = _anchor.transform.localPosition;
            _playerManager = ContentManager.Instance.Player;
            _playerInst = _playerManager.GetCurrentPlayer();
            _playerFollow = _playerInst.GetRb().transform;

            SetUpCameraPos();
        }

        private void SetUpCameraPos()
        {
            Vector3 followOffset = new Vector3(_xBaseOffset, _yBaseOffset, _zBaseOffset);
            _anchor.position = followOffset;
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

        public void FollowPlayer()
        {
            Vector3 followOffset = new Vector3(_xBaseOffset, _yBaseOffset, _zBaseOffset);
            Vector3 rotatedOffset = _playerFollow.rotation * followOffset;
            Vector3 targetPos = _playerFollow.position + rotatedOffset;
            Vector3 playerPos = Vector3.SmoothDamp(_anchor.transform.position, targetPos, ref _cameraFollowVelocity, _cameraFollowDelay, _cameraMaxSpeed, Time.deltaTime);
            _anchor.position = playerPos;
        }

        /// <summary>
        /// Camera Shake
        /// </summary>
        private void CameraDetectShake()
        {
            if (_currentLightShakeDuration <= 0) return;

            _effects.localPosition = _originalPos + Random.insideUnitSphere * _currentShakeAmount;
            _currentLightShakeDuration -= Time.deltaTime * _decreaseFactor;
        }

        public void CameraShakeLight(float? amount = null)
        {
            _currentLightShakeDuration = _lightShakeDuration;
            _currentShakeAmount = amount != null ? amount.Value : _lightShakeAmount;
        }
    }
}
