using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Create Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        public string wpnName;
        public Sprite pic;
        public WeaponType type;
        public float ammo;
        public float maxAmmo;
        public float fireRate;
        public float power;

        public Weapon(Weapon weapon1)
        {
            wpnName = weapon1.wpnName;
            pic = weapon1.pic;
            type = weapon1.type;
            ammo = weapon1.ammo;
            maxAmmo = weapon1.maxAmmo;
            fireRate = weapon1.fireRate;
            power = weapon1.power;
        }
    }
}