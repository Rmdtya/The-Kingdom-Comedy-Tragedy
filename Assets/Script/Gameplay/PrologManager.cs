using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Rmdtya
{
    public class PrologManager : MonoBehaviour
    {
        [SerializeField] private CanvasGroup panelText;
        [SerializeField] private TextMeshProUGUI textMeshKeterangan;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip audioClip;
        [SerializeField] private string[] textIsi;
        [SerializeField] private float typingSpeed;
        [SerializeField] private int currentIndex;
        [SerializeField] private Button nextButton;
        [SerializeField] private Button nextScene;

        [SerializeField] private GameObject panelProlog;
        [SerializeField] private GameObject panelStarter;
        [SerializeField] private DataLevel dataLevel;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void Start()
        {
            Time.timeScale = 1f;
            audioSource.clip = audioClip;
            audioSource.loop = true;
            audioSource.Play();
            currentIndex = 0;

            nextButton.enabled = false;
            nextButton.gameObject.SetActive(false);

            nextScene.enabled = false;
            nextScene.gameObject.SetActive(false);

            panelProlog.SetActive(true);
            panelStarter.SetActive(false);

            StartProlog();
        }

        private void StartProlog()
        {
            textMeshKeterangan.text = " ";
            panelText.gameObject.SetActive(true);
            StartCoroutine(InitialTextProlog());
        }

        private IEnumerator InitialTextProlog()
        {
            yield return new WaitForSeconds(2f);

            StartCoroutine(TypeText(textIsi[currentIndex]));
        }

        public void PlayNextText()
        {
            currentIndex++;
            nextButton.enabled = false;
            nextButton.gameObject.SetActive(false);

            StartCoroutine(TypeText(textIsi[currentIndex]));
        }

        private void FadeIn(CanvasGroup canvasGroup)
        {
            StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 1f));
        }

        private void FadeOut(CanvasGroup canvasGroup)
        {
            StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 0f));
        }

        private IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float startAlpha, float targetAlpha)
        {
            float elapsedTime = 0f;

            while (elapsedTime < 1f)
            {
                canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / 1f);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Pastikan alpha bernilai tepat pada akhir efek
            canvasGroup.alpha = targetAlpha;
        }

        private IEnumerator TypeText(string fullText)
        {
            for (int i = 0; i <= fullText.Length; i++)
            {
                string currentText = fullText.Substring(0, i);
                textMeshKeterangan.text = currentText;

                yield return new WaitForSeconds(typingSpeed);
            }

            if (currentIndex < textIsi.Length -1)
            {
                nextButton.gameObject.SetActive(true);
                nextButton.enabled = true;
            }
            else
            {
                nextButton.enabled = false;
                nextButton.gameObject.SetActive(false);

                nextScene.gameObject.SetActive(true);
                nextScene.enabled = true;
            }
        }

        public void ShowStarterPanel()
        {
            panelProlog.SetActive(false);
            panelStarter.SetActive(true);
        }

        public void NextScene(int level)
        {
            dataLevel.levelSelected = level;
            SceneManager.LoadScene("Gameplay");
        }

        public void SkipScene()
        {
            panelStarter.SetActive(true);
        }

    }
}
