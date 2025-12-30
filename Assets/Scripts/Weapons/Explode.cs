using Unity.VisualScripting;
using UnityEngine;

public class Explode : MonoBehaviour
{
    void Start()
    {
        
    }

    public void OnExplosion( WeaponDataSO data)
    {
        TerrainModify.instance.OnExplosion(transform.position, data.radius);
        Destroy(gameObject);

    }

}