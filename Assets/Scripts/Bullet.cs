using System;
using Photon.Pun;
using Script.Manager;
using UnityEngine;


public class Bullet : MonoBehaviour,IPooledObject
{
    private Transform _tr;
    public float speed;
    public string poolTag;
    public float power;
    private void Awake()
    {
        _tr = transform;
    }

    private void Update()
    {
        _tr.position += _tr.forward * (speed * Time.deltaTime);
    }

    public void OnSpawn()
    {
        
    }

    public void BackToPool()
    {
        PoolManager.Instance.BackToPool(gameObject,poolTag);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (PhotonNetwork.IsMasterClient)
            {
                other.GetComponent<PlayerHealth>().Damage(power);
                BackToPool();
            }
        }
    }
}