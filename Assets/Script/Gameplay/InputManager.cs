using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Rmdtya
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private PlayerInputs playerInputs;
        [SerializeField] private AudioSource[] itemAudioSource;

        private void Awake()
        {
            if(playerInputs == null)
            {
                playerInputs = new PlayerInputs();
                playerInputs.Enable();
            }
        }

        private void Start()
        {
            InitialInputSettings();
        }

        private void Update()
        {

        }

        private void InitialInputSettings()
        {
            Debug.Log("Input Has Initial");
            //Rythm Player Input
            playerInputs.RythmAction.Up.performed += i => RythmInput(0);
            playerInputs.RythmAction.Down.performed += i => RythmInput(1);
            playerInputs.RythmAction.Right.performed += i => RythmInput(2);
            playerInputs.RythmAction.Left.performed += i => RythmInput(3);

            //Player Item Input
            playerInputs.ItemAction.One.performed += i => ItemInput(1);
            playerInputs.ItemAction.Two.performed += i => ItemInput(2);
            playerInputs.ItemAction.Three.performed += i => ItemInput(3);
            playerInputs.ItemAction.Four.performed += i => ItemInput(4);
            playerInputs.ItemAction.Five.performed += i => ItemInput(5);
            playerInputs.ItemAction.Six.performed += i => ItemInput(6);

        }

        private void RythmInput(int inputNumber)
        {
            if(inputNumber == 0)
            {
                ActionManager.instance.ClickEvent(RythmArrow.up);
            }else if (inputNumber == 1)
            {
                ActionManager.instance.ClickEvent(RythmArrow.down);
            }
            else if (inputNumber == 2)
            {
                ActionManager.instance.ClickEvent(RythmArrow.right);
            }
            else if (inputNumber == 3)
            {
                ActionManager.instance.ClickEvent(RythmArrow.left);
            }
        }

        private void ItemInput(int itemIndex)
        {
            ActionManager.instance.PlayItem(itemIndex);
            itemAudioSource[itemIndex - 1].Play();
        }
    }
}
