using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Rmdtya
{
    public class ActionManager : MonoBehaviour
    {
        public static ActionManager instance;

        public List<RythmScript> listRythm;
        [SerializeField] private RectTransform leftPanel;
        [SerializeField] private RectTransform rightPanel;

        private float rythmSpeed;
        private float rxthmDissappearSpeed;

        private float lastRightXPosition;
        private float lastRightYPosition;
        private float lastLeftXPosition;
        private float lastLeftYPosition;

        private float xPosition;
        private float yPosition;

        [Header("LATAR BELAKANG")]
        [SerializeField] private string[] textLatarBelakang;
        [SerializeField] private bool isInitial = false;

        [Header("EVENT ITEM")]
        [SerializeField] private float timer = 0f;
        [SerializeField] private float[] eventTime;
        [SerializeField] private float[] durationEvent;
        [SerializeField] private ItemDataStatus[] dataEvent;
        [SerializeField] private int currentEvent;
        [SerializeField] private float countDownEvent;
        [SerializeField] private int changeLeft;
        [SerializeField] private int changeNow;
        [SerializeField] private bool isCountdown;
        [SerializeField] private float[] listItemAction;
        [SerializeField] private bool isEventTrigger;

        [Header("Popup Stat")]
        [SerializeField] private Sprite[] _sprite;


        [Header("COMBO")]
        [SerializeField] private int comboRythm;
        [SerializeField] private int nullAction = 0;

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }

            Application.targetFrameRate = 60;
        }

        private void Start()
        {
            isInitial = false;
            StartLatbelakang();

            timer = 0f;
            currentEvent = 0;
            isCountdown = true;
            isEventTrigger = false;
            changeLeft = dataEvent[currentEvent].change;
            listItemAction = new float[dataEvent[currentEvent].change];

            for (int i = 0; i < listItemAction.Length; i++)
            {
                listItemAction[i] = 0;
            }

            durationEvent = new float[dataEvent.Length];

            for (int i = 0; i < durationEvent.Length; i++)
            {
                durationEvent[i] = dataEvent[currentEvent].time;
            }

            isInitial = true;
            comboRythm = 0; 
        }

        public void StartGame()
        {
            rythmSpeed = GameManager.instance.rythmSpeed;
            rxthmDissappearSpeed = GameManager.instance.rythmDissappearSpeed;
            InvokeRepeating("SpawnObject", 3f, rythmSpeed);
        }

        private void Update()
        {
            if (!isInitial)
                return;

            timer += Time.deltaTime;

            // Jika telah mencapai 120 detik, lakukan sesuatu
            if (timer >= 100f)
            {
                GameManager.instance.EndGame();
            }

            if (isCountdown)
            {
                if (timer >= eventTime[currentEvent])
                {
                    isCountdown = false;
                    listItemAction = new float[dataEvent[currentEvent].change];

                    for(int i = 0;i < listItemAction.Length; i++)
                    {
                        listItemAction[i] = 0;
                    }

                    changeLeft = dataEvent[currentEvent].change;
                    isEventTrigger = true;
                    changeNow = 0;

                    GameManager.instance.UIManager.ShowEventText(dataEvent[currentEvent].textEvent, dataEvent[currentEvent].time);
                }
            }
            else
            {
                EventCheck();
            }
        }

        private void StartLatbelakang()
        {
            GameManager.instance.UIManager.ShowMultipleText(textLatarBelakang);
        }

        private void EventCheck()
        {
            if(currentEvent == 0){
                Checking(0);
            }
            else if(currentEvent == 1) {
                Checking(1);
            }
            else if(currentEvent == 2) {
                Checking(2);
            }
            else if(currentEvent == 3) {
                Checking(3);
            }
            else
            {
                Debug.Log("Selesai");
            }
        }

        private void Checking(int index)
        {
                if (timer >= eventTime[index] + durationEvent[currentEvent])
                {
                    isCountdown = true;
                    isEventTrigger = false;
                    currentEvent++;

                    ChekingItemAction();

                GameManager.instance.UpdateStatusKings();
                GameManager.instance.ChekingStatus();
            }
                else
                {
                    if (timer >= eventTime[currentEvent] && timer <= eventTime[currentEvent] + durationEvent[currentEvent])
                    {
                        if (!isEventTrigger)
                        {
                            isEventTrigger = true;
                        }
                    }
                }
        }

        private void SpawnObject()
        {
            GameObject rythmObject = RythmPool.instance.GetRyhmObject();

            if(rythmObject != null )
            {
                RythmScript script = rythmObject.GetComponent<RythmScript>();
                // Set the parent to the panel

                if (script.isleft)
                {
                    rythmObject.transform.SetParent(leftPanel);
                    GetRandomXPosition(true);
                    GetRandomYPosition(true);
                }
                else
                {
                    rythmObject.transform.SetParent(rightPanel);
                    GetRandomXPosition(false);
                    GetRandomYPosition(false);
                }

                // Set random position within the panel
                rythmObject.transform.localPosition = new Vector2(xPosition, yPosition);

                listRythm.Add(script);
                int indeks = listRythm.IndexOf(script);
                script.indexOnAction = indeks;

                rythmObject.SetActive(true);
                script.DissappearAfter();

                //Debug.Log("Has Spawn");
            }
            else
            {
                Debug.Log("Null Spawn");
            }
        }

        private void GetRandomXPosition(bool isLeft)
        {
            float randomXPosition = UnityEngine.Random.Range(-240f, 240f);

            if (isLeft)
            {
                if(Mathf.Abs(randomXPosition - lastLeftXPosition) < 75f)
                {
                    GetRandomXPosition(true);
                }
                else
                {
                    xPosition = randomXPosition;
                    lastLeftXPosition = randomXPosition;
                }
            }
            else
            {
                if (Mathf.Abs(randomXPosition - lastRightXPosition) < 75f)
                {
                    GetRandomXPosition(false);
                }
                else
                {
                    xPosition = randomXPosition;
                    lastRightXPosition = randomXPosition;
                }
            }
        }

        private void GetRandomYPosition(bool isLeft)
        {
            float randomYPosition = UnityEngine.Random.Range(-190f, 190f);

            if (isLeft)
            {
                if (Mathf.Abs(randomYPosition - lastLeftYPosition) < 75f)
                {
                    GetRandomXPosition(true);
                }
                else
                {
                    yPosition = randomYPosition;
                    lastLeftYPosition = randomYPosition;
                }
            }
            else
            {
                if (Mathf.Abs(randomYPosition - lastRightYPosition) < 75f)
                {
                    GetRandomXPosition(false);
                }
                else
                {
                    yPosition = randomYPosition;
                    lastRightYPosition = randomYPosition;
                }
            }
        }

        public void RemoveLastIndex()
        {
            listRythm.RemoveAt(listRythm.Count - 1);
        }

        public void ClickEvent(RythmArrow arrow)
        {
            bool isTrue = false;
            for(int i = 0; i < listRythm.Count; i++)
            {
                if (listRythm[i].rythmArrow == arrow)
                {
                    if (!listRythm[i].isTrue)
                    {
                        listRythm[i].HasClick();
                        isTrue = true;
                        comboRythm ++;
                        CommulateCombo();
                        return;
                    }
                }
            }

            if(!isTrue)
            {
                Debug.Log("Tidak ada yang benar");
                comboRythm = 0;
                AnimatorManager.instance.PlayTargetAnimation("Idle");
                GameManager.instance.UpdateScore(false, 2);
                SoundManager.instance.PlayMissRythm();
            }
        }

        private void CommulateCombo()
        {
            if(comboRythm == 10)
            {
                AnimatorManager.instance.PlayAgreeAnimation(0);
            }else if(comboRythm == 20)
            {
                AnimatorManager.instance.PlayAgreeAnimation(1);
            }else if(comboRythm == 50)
            {
                AnimatorManager.instance.PlayAgreeAnimation(2);
            }
        }

        public void PlayItem(int itemNumber)
        {
            if (isEventTrigger)
            {
                listItemAction[changeNow] = itemNumber;
                changeNow++;
                changeLeft--;

                if (changeLeft <= 0)
                {
                    isCountdown = true;
                    isEventTrigger = false;
                }

                float[] value = new float[3];

                value[0] = dataEvent[currentEvent].listDataInput[itemNumber - 1].amarah;
                value[1] = dataEvent[currentEvent].listDataInput[itemNumber - 1].humor;
                value[2] = dataEvent[currentEvent].listDataInput[itemNumber - 1].perasaan;

                Debug.Log("Value : " + value[0] + " - " + value[1] + " - " + value[2]);

                GameManager.instance.amarah += value[0];
                GameManager.instance.humor += value[1];
                GameManager.instance.perasaan += value[2];

                GameManager.instance.UpdateStatusKings();
                GameManager.instance.ChekingStatus();

                int random = UnityEngine.Random.Range(0, 3);
                for (int i = 0; i < value.Length; i++)
                {
                    if (value[i] > 0)
                    {
                        if (i == 0)
                        {
                            GameManager.instance.UIManager.ShowStatUp(_sprite[i], value[i], true, random);
                        }
                        else
                        {
                            GameManager.instance.UIManager.ShowStatUp(_sprite[i], value[i], false, random);
                        }
                    }
                    else if (value[i] < 0)
                    {
                        if (i == 0)
                        {
                            GameManager.instance.UIManager.ShowStatUp(_sprite[i], value[i], true, random);
                        }
                        else
                        {
                            GameManager.instance.UIManager.ShowStatUp(_sprite[i], value[i], false, random);
                        }
                    }
                }
            }
            else
            {

            }
        }

        public void ChekingItemAction()
        {
            bool isminus = false;
            for(int i = 0; i < listItemAction.Length; i++)
            {
                if(listItemAction[i] == 0)
                {
                    GameManager.instance.amarah += 10;
                    GameManager.instance.perasaan -= 10;
                    GameManager.instance.humor -= 10;

                    if (!isminus)
                    {
                        isminus = true;
                        GameManager.instance.UIManager.ShowStatUp(_sprite[0], 10, true, 1);
                        GameManager.instance.UIManager.ShowStatUp(_sprite[1], 10, false, 2);
                        GameManager.instance.UIManager.ShowStatUp(_sprite[2], 10, false, 3);
                    }
                }
            }





        }

        public void BreakCombo()
        {
            comboRythm = 0;
        }
    }
}
