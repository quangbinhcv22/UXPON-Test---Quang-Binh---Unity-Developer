using System;
using Entities;
using UnityEngine;

namespace Gameplay
{
    public class Gameplay : MonoBehaviour
    {
        public static Gameplay Instance;
        public static Character Player;
        public static bool IsPlaying;

        public static Action OnStart;
        public static Action OnEnd;
    

        private void Awake()
        {
            Instance = this;
        }

        public void StartGame()
        {
            IsPlaying = true;

            OnStart?.Invoke();

            Player.OnStartGame();
        
            Player.onDie -= OnPlayerDie;
            Player.onDie += OnPlayerDie;
        }

        private void OnPlayerDie()
        {
            EndGame();
        }

        public void EndGame()
        {
            IsPlaying = false;

            OnEnd?.Invoke();
            GameplayNetwork.Instance.DeleteAccount();
        }
    }
}