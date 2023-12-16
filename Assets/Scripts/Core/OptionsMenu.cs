using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace AsteroidsTest.Core
{
    public class OptionsMenu : MonoBehaviour
    {
        [SerializeField]
        AudioMixer _mainMixer;

        [SerializeField]
        Slider _slider1;

        [SerializeField]
        Slider _slider2;

        [SerializeField]
        Slider _slider3;

        //float _mixerValue;

        void Start()
        {
            // _mainMixer.GetFloat("MasterVolume", out _mixerValue);
            _slider1.value = PlayerPrefs.GetFloat("MasterVolume", 0.5f);
            //_mainMixer.GetFloat("BGMVolume", out _mixerValue);
            _slider2.value = PlayerPrefs.GetFloat("BGMVolume", 0.5f);
            // _mainMixer.GetFloat("SFXVolume", out _mixerValue);
            _slider3.value = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
        }

        public void SetMasterVolume(float volume)
        {
            _mainMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("MasterVolume", volume);
        }

        public void SetMusicVolume(float volume)
        {
            _mainMixer.SetFloat("BGMVolume", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("BGMVolume", volume);
        }

        public void SetSFXVolume(float volume)
        {
            _mainMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("SFXVolume", volume);
        }
    }
}
