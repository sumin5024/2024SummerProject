using UnityEngine;

public enum WeaponType
{
    Pistol,
    Shotgun
}

[System.Serializable]
public class Weapon
{
    public WeaponType weaponType;
    public GameObject bulletPrefab;
}
