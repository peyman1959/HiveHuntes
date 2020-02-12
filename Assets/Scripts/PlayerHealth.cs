using System;
using Photon.Pun;
using Script.Manager;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public bool isAlive = true;
    private PhotonView _pv;
    public Collider ccollider;
    public GameObject graphics;
    private void Start()
    {
        _pv = GetComponent<PhotonView>();
    }

    public void Damage(float power)
    {
        if (!isAlive)
            return;

        _pv.RPC("SyncHealth", RpcTarget.All, power);
    }

    private GameObject effect;

    void Death()
    {
        effect = PoolManager.Instance.GetObject("Effect");
        effect.transform.position = transform.position;
        effect.SetActive(true);
        effect.GetComponent<ParticleSystem>().Play();
        ccollider.enabled = false;
        graphics.SetActive(false);
        Invoke("reSpawn", 2f);
    }

    void reSpawn()
    {
        if (_pv.IsMine)
        {
            transform.position = LevelManager.Instance.reSpawn();
        }
        health = maxHealth;
        isAlive = true;
        ccollider.enabled = true;
        Invoke("setGraphic",0.5f);
    }

    void setGraphic()
    {
        graphics.SetActive(true);
    }
    [PunRPC]
    public void SyncHealth(float power)
    {
        health -= power;
        if (health <= 0)
        {
            isAlive = false;
            health = 0;
            Death();
        }
    }
}