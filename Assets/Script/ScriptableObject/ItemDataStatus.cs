using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rmdtya
{
    [CreateAssetMenu(fileName = "InputData", menuName = "Player/InputStatus", order = 3)]
    public class ItemDataStatus : ScriptableObject
    {
        [SerializeField] private float _time;
        [SerializeField] private int _change;
        [SerializeField] private InputDataStatus[] _listDataInput;

        [SerializeField] private string _textEvent;

        public string textEvent { get => _textEvent; set => _textEvent = value; }

        public float time { get => _time; set => _time = value; }
        public int change { get => _change; set => _change = value; }
        public InputDataStatus[] listDataInput { get => _listDataInput; set => _listDataInput = value; }

    }

    [System.Serializable]
    public struct InputDataStatus
    {
        [SerializeField] private float _amarah;
        [SerializeField] private float _humor;
        [SerializeField] private float _perasaan;

        public float amarah { get => _amarah; set => _amarah = value; }
        public float humor { get => _humor; set => _humor = value; }
        public float perasaan { get => _perasaan; set => _perasaan = value; }

    }
}
