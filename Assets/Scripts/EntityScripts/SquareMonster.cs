using UnityEngine;

public class SquareMonster : Monsters
{
    private Transform shooter;

    void Start()
    {
        shooter = GameObject.Find("Shooter").transform;
    }

    void Update()
    {
        if (Health <= 0 || shooter == null)
        {
            Destroy(gameObject);
            return;
        }
        Vector3 direction = (shooter.position - transform.position).normalized;
        transform.Translate(direction * _speed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        var projectile = collision.GetComponent<Projectiles>();
        if (projectile != null)
        {
            TakeDamage(projectile.damage);
            Destroy(collision.gameObject);
        }
    }
}
