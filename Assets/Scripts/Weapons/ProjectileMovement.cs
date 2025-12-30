using Unity.VisualScripting;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    private WeaponDataSO weaponData;
    private GameObject target;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //Logic to shoot to target
        transform.Translate(-Vector3.right * Time.deltaTime * weaponData.speed);

    }

    public void Initialize(WeaponDataSO weaponDataSO)
    {
        weaponData = weaponDataSO;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        
        if (col.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Debug.Log("Explode");
            var explosion = gameObject.AddComponent<Explode>();
            explosion.OnExplosion(weaponData);
        }
        Debug.Log(col.gameObject.tag);
    }
}
