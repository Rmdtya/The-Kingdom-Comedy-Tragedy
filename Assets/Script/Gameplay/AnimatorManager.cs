using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rmdtya
{
    public class AnimatorManager : MonoBehaviour
    {
        public static AnimatorManager instance;
        [SerializeField] Animator animatorQueen;
        [SerializeField] private GameObject[] head;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip[] laughtClip;

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
        }

        private void Start()
        {
            head[0].SetActive(true);
            head[1].SetActive(false);
        }

        public void PlayTargetAnimation(string animationName)
        {
            head[0].SetActive(true);
            head[1].SetActive(false);
            animatorQueen.CrossFade(animationName, 0.2f);
        }

        public void PlayAgreeAnimation(int index)
        {
            head[0].SetActive(false);
            head[1].SetActive(true);
            animatorQueen.CrossFade("Agree", 0.2f);

            audioSource.clip = laughtClip[index];
            audioSource.Play();
        }

    }
}
