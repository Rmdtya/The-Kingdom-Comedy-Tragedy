using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Rmdtya
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        [SerializeField] private DataLevel dataLevel;
        [SerializeField] private ActionManager _actionManager;
        [SerializeField] private UIManager _uiManager;
        [SerializeField] private SoundManager _soundManager;

        [Header("RYTHM STATUS")]
        [SerializeField] private float _rythmSpeed;
        [SerializeField] private float _rythmDissappearSpeed;

        [Header("PLAYER STATUS")]
        [SerializeField] public float _playerSkor;

        [Header("MOOD RAJA")]
        [SerializeField] private float _amarah;
        [SerializeField] private float _humor;
        [SerializeField] private float _perasaan;


        public float amarah { get => _amarah; set {

                _amarah = value;

                if(_amarah < 0)
                {
                    _amarah = 0;
                }
            } 
        
        }
        public float humor { get => _humor; set
            {
                _humor = value;

                if(_humor < 0)
                {
                    _humor = 0;
                }
            }
        }
        public float perasaan { get => _perasaan; set { 
                _perasaan = value;
                if(_perasaan < 0)
                {
                   _perasaan = 0;
                }
            } 
        }

        public float rythmSpeed { get => _rythmSpeed; set => _rythmSpeed = value; }
        public float rythmDissappearSpeed { get => _rythmDissappearSpeed; set => _rythmDissappearSpeed = value;}

        public ActionManager actionManager {  get => _actionManager;}
        public UIManager UIManager { get => _uiManager;}
        public SoundManager SoundManager { get => _soundManager;}

        public float playerScore { get => _playerSkor; set => _playerSkor = value;}

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            
            _actionManager = GetComponent<ActionManager>();
            _uiManager = GetComponent<UIManager>();
            _soundManager = GetComponent<SoundManager>();

            Application.targetFrameRate = 60;
        }

        private void Start()
        {
            LoadData();
            _playerSkor = 100f;

            _amarah = 0f;
            _humor = 30f;
            _perasaan = 40f;

            UpdateStatusKings();
        }

        private void LoadData()
        {
            if(dataLevel.levelSelected == 0)
            {
                _rythmSpeed = 2f;
                _rythmDissappearSpeed = 3.5f;
            }else if(dataLevel.levelSelected == 1)
            {
                _rythmSpeed = 1.5f;
                _rythmDissappearSpeed = 2.5f;
            }else if(dataLevel.levelSelected == 2)
            {
                _rythmSpeed = 0.5f;
                rythmDissappearSpeed = 0.9f;
            }

            actionManager.StartGame();
        }

        public void UpdateScore(bool isTrue, float multipiler)
        {
            if (isTrue)
            {

            }
            else
            {
                _playerSkor = (_playerSkor) + (-1 * multipiler);
                _uiManager.SetTextScore(_playerSkor);

                if(_playerSkor <= 0)
                {
                    GameOver();
                }

                if(_playerSkor <= 50)
                {
                    AnimatorManager.instance.PlayTargetAnimation("Angry");
                }
            }
        }

        public void PauseGame()
        {
            Time.timeScale = 0f;
            _uiManager.PauseGame();
        }

        public void ResumeGame()
        {
            Time.timeScale = 1f;
            _uiManager.ResumeGame();
        }

        public void GameOver()
        {
            Time.timeScale = 0f;
            _uiManager.GameOver();
        }

        public void EndGame()
        {
            _soundManager.StopBacksoundMenu();
            if((amarah * 2) <= (_humor + perasaan)) {
                _soundManager.PlayAudioEnding(1);
                _uiManager.ShowPanelEnding(1);
            }else if((amarah == 0) && (_humor >= 100) && (_perasaan >= 100)){
                _soundManager.PlayAudioEnding(2);
                _uiManager.ShowPanelEnding(2);
            }
            else if((amarah * 2) >= (_humor + perasaan))
            {
                _soundManager.PlayAudioEnding(0);
                _uiManager.ShowPanelEnding(0);
            }
        }

        public void UpdateStatusKings()
        {
            _uiManager.UpdateAmarah(_amarah);
            _uiManager.UpdateHumor(_humor);
            _uiManager.UpdatePerasaan(_perasaan);
        }

        public void BackToMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void ChekingStatus()
        {
            if(_amarah >= 100)
            {
                GameOver();
            }

            if(_humor <= 0 && _perasaan <= 0) {
                GameOver();
            }
        }
    }
}
