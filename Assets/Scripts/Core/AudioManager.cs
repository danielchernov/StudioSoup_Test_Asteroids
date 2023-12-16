using UnityEngine;
using AsteroidsTest.Game;
using Random = UnityEngine.Random;

namespace AsteroidsTest.Core
{
    public class AudioManager : MonoBehaviour
    {
        private static AudioManager _instance;
        public static AudioManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("AudioManager is Null");
                }

                return _instance;
            }
        }

        public enum AudioType
        {
            Fire,
            PlayerDeath,
            EnemyDeath,
            NewHighscore,
            GameOver
        }

        [SerializeField]
        AudioSource _bgmAudio;

        [SerializeField]
        AudioSource _sfxAudio;

        [SerializeField]
        AudioClip[] _gameOverBGM;

        [SerializeField]
        AudioClip[] _fireSFX;

        [SerializeField]
        AudioClip[] _playerDeathSFX;

        [SerializeField]
        AudioClip[] _enemyDeathSFX;

        private void Awake()
        {
            _instance = this;
        }

        // Play SFX Depending on AudioType
        public void PlaySFX(AudioType audioType)
        {
            if (_sfxAudio == null)
                return;

            switch (audioType)
            {
                case AudioType.Fire:
                    _sfxAudio.PlayOneShot(_fireSFX[Random.Range(0, _fireSFX.Length)], 0.5f);
                    break;
                case AudioType.PlayerDeath:
                    _sfxAudio.PlayOneShot(
                        _playerDeathSFX[Random.Range(0, _playerDeathSFX.Length)],
                        0.5f
                    );
                    break;
                case AudioType.EnemyDeath:
                    _sfxAudio.PlayOneShot(
                        _enemyDeathSFX[Random.Range(0, _enemyDeathSFX.Length)],
                        0.5f
                    );
                    break;
                default:
                    break;
            }
        }

        // Play BGM Depending on AudioType
        public void PlayBGM(AudioType audioType)
        {
            if (_bgmAudio == null)
                return;
            _bgmAudio.loop = false;
            switch (audioType)
            {
                case AudioType.NewHighscore:
                    _bgmAudio.clip = _gameOverBGM[0];
                    break;
                case AudioType.GameOver:
                    _bgmAudio.clip = _gameOverBGM[1];
                    break;
                default:
                    break;
            }
            _bgmAudio.Play();
        }
    }
}
