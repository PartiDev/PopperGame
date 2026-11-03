namespace Assets.Scripts.Helpers
{
    using UnityEngine;

    public class DestroyAfter : MonoBehaviour
    {
        private float _destroyAfterSeconds;
        private bool _init;

        public void OnAwake(float destroyAfterSeconds)
        {
            _destroyAfterSeconds = destroyAfterSeconds;
            _init = true;
        }

        public void Update()
        {
            _destroyAfterSeconds -= Time.deltaTime;

            if (_destroyAfterSeconds <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}