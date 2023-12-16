using UnityEngine;
using UnityEngine.Audio;

namespace AsteroidsTest.UI
{
    public class OptionsMenu : MonoBehaviour
    {
        [SerializeField]
        AudioMixer _mainMixer;

        public void SetMasterVolume(float volume)
        {
            if (_mainMixer == null)
                return;
            _mainMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("MasterVolume", volume);
        }

        public void SetMusicVolume(float volume)
        {
            if (_mainMixer == null)
                return;
            _mainMixer.SetFloat("BGMVolume", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("BGMVolume", volume);
        }

        public void SetSFXVolume(float volume)
        {
            if (_mainMixer == null)
                return;
            _mainMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("SFXVolume", volume);
        }
    }
}
