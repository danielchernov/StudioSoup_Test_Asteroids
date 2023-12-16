using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using AsteroidsTest.Game;

namespace AsteroidsTest.Core
{
    public class UIManager : MonoBehaviour
    {
        private static UIManager _instance;
        public static UIManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("UIManager is Null");
                }

                return _instance;
            }
        }

        [SerializeField]
        TextMeshProUGUI _scoreText;

        [SerializeField]
        TextMeshProUGUI _livesText;

        [SerializeField]
        TextMeshProUGUI _bestText;

        [SerializeField]
        GameObject _pauseMenu;

        [SerializeField]
        GameObject _optionsMenu;

        [SerializeField]
        GameObject _gameOverMenu;

        void Awake()
        {
            _instance = this;
        }

        void Start()
        {
            // HUD Setup
            if (_scoreText == null)
                return;

            ChangeScoreText(0);
            ChangeLivesText(PlayerController.Instance.GetMaxHealth());
            ChangeHighscoreText(PlayerPrefs.GetInt("Highscore", 0));
        }

        //Add Listeners
        void OnEnable()
        {
            PlayerController.OnTookDamage += ChangeLivesText;
            AsteroidController.OnEnemyDeath += ChangeScoreText;
        }

        void OnDisable()
        {
            PlayerController.OnTookDamage -= ChangeLivesText;
            AsteroidController.OnEnemyDeath -= ChangeScoreText;
        }

        public void GameOverUI()
        {
            _gameOverMenu.SetActive(true);
        }

        public void PauseMenuUI()
        {
            _pauseMenu.SetActive(!_pauseMenu.activeSelf);
            if (_optionsMenu.activeSelf)
            {
                _optionsMenu.SetActive(false);
            }
        }

        public void StartButton()
        {
            SceneManager.LoadScene("MainLevel");
        }

        public void ExitToMenuButton()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("MainMenu");
        }

        public void QuitButton()
        {
            Application.Quit();
        }

        void ChangeLivesText(int lives)
        {
            _livesText.text = "Lives: " + lives;
        }

        void ChangeScoreText(int score)
        {
            _scoreText.text = "Score: " + score;
        }

        void ChangeHighscoreText(int best)
        {
            _bestText.text = "Best: " + best;
        }
    }
}
