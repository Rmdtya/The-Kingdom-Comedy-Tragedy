using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

namespace Rmdtya
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textScore;

        [SerializeField] private TextMeshProUGUI _textAmarah;
        [SerializeField] private TextMeshProUGUI _textHumor;
        [SerializeField] private TextMeshProUGUI _textPerasaan;

        [SerializeField] private Slider moodSlider;

        [Header("Screen Popup")]
        [SerializeField] private GameObject pausePanel;
        [SerializeField] private GameObject gameOverPanel;

        [Header("Notification Event Popup")]
        [SerializeField] private GameObject panelEvent;
        [SerializeField] private float typingSpeed = 0.01f;
        [SerializeField] private TextMeshProUGUI textEvent;

        [Header("Panel Stat Up")]
        [SerializeField] private GameObject[] panelstatup;
        [SerializeField] private Image[] imgIcon;
        [SerializeField] private CanvasGroup[] canvasGroup;
        [SerializeField] private TextMeshProUGUI[] textUpStat;

        [Header("ENDING PANEL")]
        [SerializeField] private GameObject[] panelEnding;


        private void Start()
        {
            moodSlider.maxValue = 100f;
            moodSlider.value = 100;

            for(int i = 0; i < textUpStat.Length; i++)
            {
                panelstatup[i].gameObject.SetActive(false);
            }
        }
        public void SetTextScore(float score)
        {
            moodSlider.value = score;
        }

        public void PauseGame()
        {
            pausePanel.SetActive(true);
        }

        public void GameOver()
        {
            gameOverPanel.SetActive(true);
            panelEnding[0].SetActive(true);
            SoundManager.instance.StopBacksoundMenu();
            SoundManager.instance.PlayAudioEnding(0);
        }

        public void ResumeGame()
        {
            pausePanel.SetActive(false);
        }

        public void ShowEventText(string text, float time)
        {
            panelEvent.SetActive(true);
            textEvent.text = "";
            StartCoroutine(TypeText(text));
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

        IEnumerator TypeText(string fullText)
        {
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i <= fullText.Length; i++)
            {
                string currentText = fullText.Substring(0, i);
                textEvent.text = currentText;

                yield return new WaitForSeconds(typingSpeed);
            }
        }

        private IEnumerator HidePanelEvent(float time)
        {
            yield return new WaitForSeconds(time);
            panelEvent.SetActive(false);
        }

        public void ShowMultipleText(string[] text)
        {
            panelEvent.SetActive(true);
            textEvent.text = "";
            StartCoroutine(MultipleTypeText(text));
        }

        IEnumerator MultipleTypeText(string[] fullText)
        {
            yield return new WaitForSeconds(0.5f);
            for(int i = 0; i < fullText.Length; i++)
            {
                for (int j = 0; j <= fullText[i].Length; j++)
                {
                    string currentText = fullText[i].Substring(0, j);
                    textEvent.text = currentText;

                    yield return new WaitForSeconds(typingSpeed / 3f);
                }
                yield return new WaitForSeconds(1f);
            }

        }

        public void ShowStatUp(Sprite spriteIcon, float statValue, bool isAmarah, int index)
        {
            if(isAmarah){
                imgIcon[index].sprite = spriteIcon;
                textUpStat[index].text = statValue.ToString();

                if(statValue > 0){
                    textUpStat[index].color = Color.red;
                }else if(statValue < 0)
                {
                    textUpStat[index].color = Color.green;
                }
            }
            else
            {
                imgIcon[index].sprite = spriteIcon;
                textUpStat[index].text = statValue.ToString();

                if (statValue < 0)
                {
                    textUpStat[index].color = Color.red;
                }
                else if (statValue > 0)
                {
                    textUpStat[index].color = Color.green;
                }
            }

            panelstatup[index].SetActive(true);
            canvasGroup[index].alpha = 0f;

            StartCoroutine(FadeCanvasGroup(canvasGroup[index], canvasGroup[index].alpha, 1f));
            StartCoroutine(HidePopupStat(panelstatup[index]));
        }

        private IEnumerator HidePopupStat(GameObject obj)
        {
            yield return new WaitForSeconds(4f);
            obj.SetActive(false);
        }

        public void UpdateAmarah(float value)
        {
            _textAmarah.text = value.ToString();
        }

        public void UpdateHumor(float value)
        {
            _textHumor.text = value.ToString();
        }

        public void UpdatePerasaan(float value)
        {
            _textPerasaan.text = value.ToString();
        }

        public void ShowPanelEnding(int index)
        {
            gameOverPanel.SetActive(true);
            panelEnding[index].SetActive(true);
        }
    }
}
