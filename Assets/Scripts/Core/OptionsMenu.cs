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

        float _mixerValue;

        void Start()
        {
            _mainMixer.GetFloat("MasterVolume", out _mixerValue);
            _slider1.value = Mathf.InverseLerp(-81f, 0f, Mathf.Pow(10, _mixerValue));
            _mainMixer.GetFloat("BGMVolume", out _mixerValue);
            _slider2.value = Mathf.InverseLerp(-81f, 0f, Mathf.Pow(10, _mixerValue));
            _mainMixer.GetFloat("SFXVolume", out _mixerValue);
            _slider3.value = Mathf.InverseLerp(-81f, 0f, Mathf.Pow(10, _mixerValue));
        }

        public void SetMasterVolume(float volume)
        {
            _mainMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        }

        public void SetMusicVolume(float volume)
        {
            _mainMixer.SetFloat("BGMVolume", Mathf.Log10(volume) * 20);
        }

        public void SetSFXVolume(float volume)
        {
            _mainMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        }
    }
}
