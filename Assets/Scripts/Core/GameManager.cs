using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
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

        [SerializeField]
        InputActionReference _escapeInput;

        [SerializeField]
        AudioSource _bgmAudio;

        int _currentScore = 0;

        void Awake()
        {
            _instance = this;
        }

        // Add Listeners
        void OnEnable()
        {
            AsteroidController.OnEnemyDeath += AddScore;
            PlayerController.OnPlayerDeath += CallForGameOver;

            _escapeInput.action.started += PressedEscapeInput;
        }

        void OnDisable()
        {
            AsteroidController.OnEnemyDeath -= AddScore;
            PlayerController.OnPlayerDeath -= CallForGameOver;

            _escapeInput.action.started -= PressedEscapeInput;
        }

        public void OpenPauseMenu()
        {
            UIManager.Instance.PauseMenuUI();
            PlayerController.Instance.LockInput();

            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }

        public int GetCurrentScore()
        {
            return _currentScore;
        }

        void PressedEscapeInput(InputAction.CallbackContext obj)
        {
            OpenPauseMenu();
        }

        void AddScore(int score)
        {
            _currentScore += score;
            UIManager.Instance.ChangeScoreText(_currentScore);
        }

        void CallForGameOver()
        {
            StartCoroutine(GameOver());
        }

        IEnumerator GameOver()
        {
            yield return new WaitForSeconds(1.5f);
            Time.timeScale = 0;
            UIManager.Instance.GameOverUI();
        }
    }
}
