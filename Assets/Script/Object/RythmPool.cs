using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rmdtya
{
    public class RythmPool : MonoBehaviour
    {
        private static RythmPool _instance;
        [SerializeField] private GameObject[] _rythmPrefab;
        [SerializeField] private int _amountRythm = 5;
        [SerializeField] private List<GameObject> _rythmPool = new List<GameObject>();
        [SerializeField] private Transform _rightPanel;
        [SerializeField] private Transform _LeftPanel;

        public static RythmPool instance
        {
            get
            {
                // Jika _instance belum diinisialisasi, inisialisasi instance baru
                if (_instance == null)
                {
                    _instance = FindObjectOfType<RythmPool>();

                    // Jika tidak ditemukan, buat instance baru jika memungkinkan
                    if (_instance == null)
                    {
                        GameObject obj = new GameObject("Bullet Pool");
                        _instance = obj.AddComponent<RythmPool>();
                    }
                }

                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }

            InitialPool();
        }

        private void InitialPool()
        {
            //For Right Panel
            for(int i = 0; i < _rythmPrefab.Length; i++)
            {
                for (int j = 0; j < _amountRythm; j++)
                {
                    GameObject rythmObject = Instantiate(_rythmPrefab[i]);
                    rythmObject.SetActive(false);
                    rythmObject.transform.SetParent(_rightPanel);
                    _rythmPool.Add(rythmObject);

                    RythmScript script = rythmObject.GetComponent<RythmScript>();
                    script.isleft = false;
                }
            }

            //For Left Panel
            for (int i = 0; i < _rythmPrefab.Length; i++)
            {
                for (int j = 0; j < _amountRythm; j++)
                {
                    GameObject rythmObject = Instantiate(_rythmPrefab[i]);
                    rythmObject.SetActive(false);
                    rythmObject.transform.SetParent(_LeftPanel);
                    _rythmPool.Add(rythmObject);

                    RythmScript script = rythmObject.GetComponent<RythmScript>();
                    script.isleft = true;
                }
            }
        }

        public GameObject GetRyhmObject()
        {
            return GetRandomObject();
        }

        public GameObject GetRandomObject()
        {
            // Buat daftar indeks objek yang aktifInHierarchy false
            List<int> indeksTersedia = new List<int>();

            for (int i = 0; i < _rythmPool.Count; i++)
            {
                if (!_rythmPool[i].activeInHierarchy)
                {
                    indeksTersedia.Add(i);
                }
            }

            // Jika ada objek yang tersedia, pilih satu secara acak
            if (indeksTersedia.Count > 0)
            {
                int indeksAcak = Random.Range(0, indeksTersedia.Count);
                return _rythmPool[indeksTersedia[indeksAcak]];
            }
            else
            {
                return null; // Tidak ada objek yang tersedia
            }
        }
    }
}
