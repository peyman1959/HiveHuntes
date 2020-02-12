using System;
using Script.Manager;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class LootboxManager : MonoBehaviour
    {
        public static bool instanc;
        public Weapon[] weapons;
        private void Start()
        {
            InvokeRepeating("SetLoot",3f,3f);
        }

        private GameObject temp;
        void SetLoot()
        {
            if (instanc)
            return;

            temp = PoolManager.Instance.GetObject("Lootbox");
            temp.transform.position = transform.position;
            temp.GetComponent<LootBox>().weapon = weapons[Random.Range(0, weapons.Length)];
            temp.SetActive(true);
            instanc = true;
        }
    }
}