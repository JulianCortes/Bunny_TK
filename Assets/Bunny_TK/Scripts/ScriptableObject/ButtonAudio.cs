using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Bunny_TK.DataDriven
{

    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(AudioSource))]
    public class ButtonAudio : MonoBehaviour
    {
        public AudioEvent audioEvent;

        private Button _button;
        private AudioSource _audioSource;

        private void OnEnable()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);

            _audioSource = GetComponent<AudioSource>();
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            audioEvent.Play(_audioSource);
        }
    }
}
