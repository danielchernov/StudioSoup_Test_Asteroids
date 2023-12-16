using UnityEngine;
using AsteroidsTest.Game;

namespace AsteroidsTest.UI
{
    public class HUD : MonoBehaviour
    {
        void Start()
        {
            // HUD Setup
            UIManager.Instance.ChangeScoreText(0);
            UIManager.Instance.ChangeLivesText(PlayerController.Instance.GetMaxHealth());
            UIManager.Instance.ChangeHighscoreText(PlayerPrefs.GetInt("Highscore", 0));
        }
    }
}
