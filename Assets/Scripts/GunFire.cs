using UnityEngine;

public class GunFire : MonoBehaviour
{
    public GameObject bulletPrefab;   // assign in Inspector
    private float fireRate = 2f;     // bullets per second
    private float nextFireTime = 0f;  // timer

    void Update()
    {
        // Auto-fire (no key needed)
        if (Time.time >= nextFireTime)
        {
            Fire();
            nextFireTime = Time.time + fireRate; // schedule next shot
        }
    }

    void Fire()
    {
        Instantiate(bulletPrefab, transform.position, transform.rotation);
    }
}
