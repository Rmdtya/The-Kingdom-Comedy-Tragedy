using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rmatya
{
    public class ItemScript : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _audioClip;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlaySound()
        {
            _audioSource.Play();
        }
    }
}
