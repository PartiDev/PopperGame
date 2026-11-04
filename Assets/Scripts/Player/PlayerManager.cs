namespace Assets.Scripts.Player
{
    using Assets.Scripts.Helpers;
    using Generic;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private Player _player_P;
        [SerializeField] private float _playerHeight;

        private List<Player> _players;

        private void Awake()
        {
            _players = new List<Player>();
            
            if (HelperFunctions.TryInstantiate(
                _player_P,
                out Player player,
                "Player",
                new Vector3(0, _playerHeight, 0),
                Quaternion.identity,
                this.transform))
            {
                _players.Add(player);
            }
        }

        public Player GetCurrentPlayer()
        {
            if (_players == null || _players.Count == 0)
                Debug.LogError("Difficult to play a game without any players..");

            return _players.First();
        }

        public void SetCurrentPlayerState(PlayerState newState)
        {
            var currentPlayer = GetCurrentPlayer();
            currentPlayer.SetPlayerState(newState);
        }

        public void GetCurrentPlayerState()
        {
            var currentPlayer = GetCurrentPlayer();
            currentPlayer.GetPlayerState();
        }

        public void PauseGame()
        {
            foreach (var player in _players)
            {
                player.OnPause();
            }
        }

        public void UnpauseGame()
        {
            foreach (var player in _players)
            {
                player.OnUnpause();
            }
        }
    }
}