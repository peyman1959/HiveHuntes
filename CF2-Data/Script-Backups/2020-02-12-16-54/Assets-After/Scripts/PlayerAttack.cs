using System;
using DefaultNamespace;
using Photon.Pun;
using UnityEditor;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerManager _playerManager;
    private PlayerHealth _playerHealth;
    public Weapon weapon;

    private void Awake()
    {
        _playerHealth = GetComponent<PlayerHealth>();
        _playerManager = GetComponent<PlayerManager>();
    }

    private void Start()
    {
        GetWeapon(weapon);
    }

    private void Update()
    {
        if (!_playerHealth.isAlive||LevelManager.Instance.finished)
            return;

        shooting();
    }

    private float nextShot = 0;

    void shooting()
    {
        if (weapon == null || weapon.ammo == 0)
            return;

        if (weapon.type == WeaponType.Pistol)
        {
            if (ControlFreak2.CF2Input.GetKeyDown(KeyCode.F))
            {
                _playerManager._pv.RPC("shoot", RpcTarget.All, weapon.power);
                weapon.ammo--;
                AmmoSlider();
            }
        }

        if (weapon.type == WeaponType.AssaultRifle)
        {
            if (ControlFreak2.CF2Input.GetKey(KeyCode.F))
            {
                if (Time.time > nextShot)
                {
                    _playerManager._pv.RPC("shoot", RpcTarget.All, weapon.power);
                    weapon.ammo--;
                    AmmoSlider();
                    nextShot = Time.time + weapon.fireRate;
                }
            }
        }
    }

    void AmmoSlider()
    {
        float fill =
            weapon.ammo / weapon.maxAmmo;
        Debug.Log( weapon.ammo +"/"+ weapon.maxAmmo+"="+fill);
        LevelUiManager.Instance.ammoSlider.fillAmount = fill;
    }

    public void GetWeapon(Weapon weapon1)
    {
        weapon = new Weapon(weapon1);
        LevelUiManager.Instance.setWeapons(weapon.wpnName);
    }
}