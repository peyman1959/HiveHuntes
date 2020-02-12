using System;
using Photon.Pun;
using Script.Manager;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerManager : MonoBehaviour
{
    public Renderer body;
    public Transform muzzle;
    public GameObject sight;
    private PlayerMovement _playerMovement;
    private PlayerAttack _playerAttack;
    private PlayerHealth _playerHealth;
    public GameObject cam;
    [HideInInspector]
    public PhotonView _pv;

    private void Start()
    {
        ControlFreak2.CFCursor.visible = false;
        _pv = GetComponent<PhotonView>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerAttack = GetComponent<PlayerAttack>();
        _playerHealth = GetComponent<PlayerHealth>();
        if (!_pv.IsMine)
        {
            Destroy(_playerMovement);
            Destroy(_playerAttack);
            Destroy(cam);
            Destroy(sight);
        }
    }

    private GameObject bullet;
    [PunRPC]
    public void shoot(float power)
    {
        bullet= PoolManager.Instance.GetObject("Bullet");
        bullet.transform.position = muzzle.position;
        bullet.transform.rotation = muzzle.rotation;
        bullet.SetActive(true);
        bullet.GetComponent<Bullet>().power = power;
    }
}