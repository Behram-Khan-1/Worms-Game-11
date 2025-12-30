using Unity.VisualScripting;
using UnityEngine;

public abstract class Weapons : MonoBehaviour
{
    public WeaponDataSO weaponData;
    public abstract void Fire(Vector3 spawnPoint);

}
