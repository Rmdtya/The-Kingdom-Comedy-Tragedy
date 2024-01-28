using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rmdtya
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager instance;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip backsoundGameplay;
        [SerializeField] private AudioClip backsoundMenu;


        [SerializeField] private AudioSource missRythm;
        [SerializeField] private AudioClip missClip;

        [SerializeField] private AudioClip[] audioEnding;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();

            PlayBacksoundGameplay();
        }

        public void PlayBacksoundGameplay()
        {
            _audioSource.clip = backsoundGameplay;
            _audioSource.loop = true;
            _audioSource.Play();
        }

        public void StopBacksoundMenu() {
            _audioSource.Stop();
        }

        public void PlaySoundMenu()
        {
            _audioSource.clip = backsoundMenu;
            _audioSource.Play();
        }

        public void PlayMissRythm()
        {
            missRythm.clip = missClip;
            missRythm.Play();
        }

        public void PlayAudioEnding(int index)
        {
            _audioSource.Stop();
            _audioSource.clip = audioEnding[index];
            _audioSource.loop = true;
            _audioSource.Play();
        }
    }
}
