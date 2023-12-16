using UnityEngine;
using UnityEngine.SceneManagement;
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

        private void Awake()
        {
            _instance = this;
        }

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

        public void StartButton()
        {
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

        public void GameOverUI()
        {
            Debug.Log("UI Game Over Active");
        }

        void ChangeLivesText()
        {
            Debug.Log("Changed Lives UI Text!");
        }

        void ChangeScoreText()
        {
            Debug.Log("Changed Score UI Text!");
        }
    }
}
