using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rmdtya
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Player/DataLevel", order = 2)]
    public class DataLevel : ScriptableObject
    {
        [SerializeField] private int _levelSelected;

        public int levelSelected { get => _levelSelected; set => _levelSelected = value;}
    }
}
