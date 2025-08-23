using UnityEngine;

public class Monster : MonoBehaviour
{
    private float speed = 2f;
    private int health = 10;
    private Transform shooter;
    void Start()
    {
        shooter = GameObject.Find("Shooter").transform;
    }
    void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
        Vector3 direction = (shooter.position - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Bullet>(out var bullet))
        {
            health -= bullet.damage;
            Destroy(bullet.gameObject);
        }
    }
}
