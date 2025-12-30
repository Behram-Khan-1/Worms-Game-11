using Unity.VisualScripting;
using UnityEngine;

public class Bomb : Weapons
{
    public GameObject prefab;
    public override void Fire(Vector3 spawnpoint)
    {
       GameObject newBomb = Instantiate(prefab, spawnpoint, Quaternion.identity);
       var projMovement = newBomb.AddComponent<ProjectileMovement>();
       projMovement.Initialize(weaponData);
       
    }

}