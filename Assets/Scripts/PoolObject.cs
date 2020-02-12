using System;
using System.Collections;
using System.Collections.Generic;
using Script.Manager;
using UnityEngine;

public class PoolObject : MonoBehaviour,IPooledObject
{
    public string poolTag;
    public float timeBack;
    private void OnEnable()
    {
Invoke("BackToPool",timeBack);    }

    public void OnSpawn()
    {
    }

    public void BackToPool()
    {
        PoolManager.Instance.BackToPool(gameObject,poolTag);
    }
}
