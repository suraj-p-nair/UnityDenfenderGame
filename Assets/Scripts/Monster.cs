using UnityEngine;

public class Monster : MonoBehaviour
{
    private float speed = 3f;
    //private int health = 10;
    private Transform shooter;
    void Start()
    {
        shooter = GameObject.Find("Shooter").transform;
        Destroy(gameObject, 2.5f);
    }
    void Update()
    {
        // Move bullet forward every frame
        Vector3 direction = (shooter.position - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
