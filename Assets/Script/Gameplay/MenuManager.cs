using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Rmdtya
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip clip;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            audioSource.clip = clip;
            audioSource.loop = true;
            audioSource.Play();
        }

        public void ChangeScene()
        {
            SceneManager.LoadScene("Prolog");
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
