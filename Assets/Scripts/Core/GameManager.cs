using UnityEngine;
using AsteroidsTest.Game;

namespace AsteroidsTest.Core
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("GameManager is Null");
                }

                return _instance;
            }
        }

        private void Awake()
        {
            _instance = this;
        }

        void OnEnable()
        {
            PlayerController.OnPlayerDeath += GameOver;
        }

        void OnDisable()
        {
            PlayerController.OnPlayerDeath -= GameOver;
        }

        void GameOver()
        {
            Debug.Log("Game Over");
            UIManager.Instance.GameOverUI();
        }
    }
}
