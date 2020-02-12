using System;
using Script.Manager;
using UnityEngine;

namespace DefaultNamespace
{
    public class LootBox : MonoBehaviour,IPooledObject
    {
        public Weapon weapon;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (other.GetComponent<PlayerAttack>())
                {
                    other.GetComponent<PlayerAttack>().GetWeapon(weapon);
                }

                LootboxManager.instanc = false;
                BackToPool();
                
            }
        }

        public void OnSpawn()
        {
        }

        public void BackToPool()
        {
            
            PoolManager.Instance.BackToPool(gameObject,"Lootbox");
        }
    }
}