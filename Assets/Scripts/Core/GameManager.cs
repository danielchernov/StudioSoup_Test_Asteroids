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

        [SerializeField]
        AudioClip _gameOverBGM;

        private void Awake()
        {
            _instance = this;
        }

        // Add Listeners
        void OnEnable()
        {
            PlayerController.OnPlayerDeath += GameOver;
            _escapeInput.action.started += PressedEscapeInput;
        }

        void OnDisable()
        {
            PlayerController.OnPlayerDeath -= GameOver;
            _escapeInput.action.started -= PressedEscapeInput;
        }

        void GameOver()
        {
            Debug.Log("Game Over");
            Time.timeScale = 0;
            UIManager.Instance.GameOverUI();
            _bgmAudio.clip = _gameOverBGM;
            _bgmAudio.Play();
        }

        void PressedEscapeInput(InputAction.CallbackContext obj)
        {
            OpenPauseMenu();
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
    }
}
