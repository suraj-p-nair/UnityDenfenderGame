using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 5f;

    void Start()
    {
        Destroy(gameObject, 2f); // bullet disappears after 2 seconds
    }
    void Update()
    {
        // Move bullet forward every frame
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
}
