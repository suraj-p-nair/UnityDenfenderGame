using UnityEngine;

public class GunFire : MonoBehaviour
{
    public GameObject bulletPrefab;   // assign in Inspector
    private float fireRate = 0.5f;
    private float fireTimer = 0f;

    void Update()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireRate)
        {
            Fire();
            fireTimer = 0f;
        }
    }

    void Fire()
    {
        if (MonsterFactory.Instance == null) return;
        if (!MonsterFactory.Instance.AnyMonstersExist()) return;

        Instantiate(bulletPrefab, transform.position, transform.rotation);
    }
}
