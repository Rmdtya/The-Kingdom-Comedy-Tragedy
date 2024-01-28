using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Rmdtya
{
    public class RythmScript : MonoBehaviour
    {
        [SerializeField] private RythmArrow _rythmArrow;
        [SerializeField] private bool _isLeft = false;
        [SerializeField] private int _indexOnAction;
        [SerializeField] private bool isPool = true;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip clickEffect;
        [SerializeField] private AudioClip missEffect;
        [SerializeField] private RythmScript _script;
        [SerializeField] private Image image;
        public bool isTrue = false;

        public bool isleft { get => _isLeft; set => _isLeft = value; }
        public RythmArrow rythmArrow { get => _rythmArrow;}

        public int indexOnAction { get => _indexOnAction; set => _indexOnAction = value;}

        private void OnEnable()
        {
            if (isPool)
            {
                isPool = false;
            }
            else
            {
                isTrue = false;
                _script.enabled = true;
                image.enabled = true;
                _audioSource.enabled = true;
            }
        }

        private void Awake()
        {
            Application.targetFrameRate = 60;
            _audioSource = GetComponent<AudioSource>();
            image = GetComponent<Image>();
            _script = this;
            isPool = true;
        }

        public void HasClick()
        {
            _audioSource.clip = clickEffect;
            _audioSource.Play();
            GameManager.instance.UpdateScore(true, 1);
            isTrue = true;
            image.enabled = false;
            StartCoroutine(TrueAnswer());
        }

        private IEnumerator TrueAnswer()
        {
            yield return new WaitForSeconds(0.5f);
            _audioSource.enabled = false;
            _audioSource.clip = null;
            RemoveAtList(true);
        }

        public void DissappearAfter()
        {
            StartCoroutine(Dissappear(GameManager.instance.rythmDissappearSpeed));
        }

        private IEnumerator Dissappear(float time)
        {
            yield return new WaitForSeconds(time);

            if (!isTrue)
            {
                RemoveAtList(false);
                ActionManager.instance.BreakCombo();
            }
        }

        private void RemoveAtList(bool isTrue)
        {
            ActionManager.instance.listRythm.RemoveAt(indexOnAction);
            int length = ActionManager.instance.listRythm.Count;

            // Menaikkan indeks di atasnya
            for (int i = _indexOnAction; i < length; i++)
            {
                int index = i;
                RythmScript script = ActionManager.instance.listRythm[i];
                script.indexOnAction = index;
            }

            if(!isTrue) {
                GameManager.instance.UpdateScore(false, 3);
                gameObject.SetActive(false);

            }
            else
            {
                gameObject.SetActive(false);
            }

        }
    }

    public enum RythmArrow
    {
        up, down, left, right
    }
}


