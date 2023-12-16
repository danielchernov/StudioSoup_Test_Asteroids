using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using AsteroidsTest.Core;
using AsteroidsTest.Game;

namespace AsteroidsTest.UI
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
        TextMeshProUGUI _highscoreGameOverText;

        [SerializeField]
        GameObject _pauseMenu;

        [SerializeField]
        GameObject _optionsMenu;

        [SerializeField]
        GameObject _gameOverMenu;

        [SerializeField]
        Slider _slider1;

        [SerializeField]
        Slider _slider2;

        [SerializeField]
        Slider _slider3;

        void Awake()
        {
            _instance = this;
        }

        void Start()
        {
            //Sets Volume Sliders Up
            if (_slider1 == null)
                return;
            _slider1.value = PlayerPrefs.GetFloat("MasterVolume", 1);
            if (_slider2 == null)
                return;
            _slider2.value = PlayerPrefs.GetFloat("BGMVolume", 1);
            if (_slider3 == null)
                return;
            _slider3.value = PlayerPrefs.GetFloat("SFXVolume", 1);
        }

        //Add Listeners
        void OnEnable()
        {
            PlayerController.OnTookDamage += ChangeLivesText;
        }

        void OnDisable()
        {
            PlayerController.OnTookDamage -= ChangeLivesText;
        }

        public void GameOverUI()
        {
            if (_gameOverMenu == null)
                return;

            _gameOverMenu.SetActive(true);

            if (_highscoreGameOverText == null)
                return;

            if (GameManager.Instance.GetCurrentScore() >= PlayerPrefs.GetInt("Highscore"))
            {
                ChangeHighscoreText(GameManager.Instance.GetCurrentScore());
                AudioManager.Instance.PlayBGM(AudioManager.AudioType.NewHighscore);
                _highscoreGameOverText.gameObject.SetActive(true);
                _highscoreGameOverText.text =
                    "Nuevo Mejor Puntaje: " + GameManager.Instance.GetCurrentScore();
            }
            else
            {
                AudioManager.Instance.PlayBGM(AudioManager.AudioType.GameOver);
                _highscoreGameOverText.gameObject.SetActive(false);
            }
        }

        public void PauseMenuUI()
        {
            if (_pauseMenu == null)
                return;

            _pauseMenu.SetActive(!_pauseMenu.activeSelf);

            if (_optionsMenu == null)
                return;

            if (_optionsMenu.activeSelf)
            {
                _optionsMenu.SetActive(false);
            }
        }

        public void StartButton()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("MainLevel");
        }

        public void ExitToMenuButton()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void QuitButton()
        {
            Application.Quit();
        }

        public void ChangeLivesText(int lives)
        {
            if (_livesText == null)
                return;
            _livesText.text = "Vida: " + lives;
        }

        public void ChangeScoreText(int score)
        {
            if (_scoreText == null)
                return;
            _scoreText.text = "Puntaje: " + score;
        }

        public void ChangeHighscoreText(int currentScore)
        {
            if (_bestText == null)
                return;
            _bestText.text = "Highscore: " + currentScore;
            PlayerPrefs.SetInt("Highscore", currentScore);
        }
    }
}
