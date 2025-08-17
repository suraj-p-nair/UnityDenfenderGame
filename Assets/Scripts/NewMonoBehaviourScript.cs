using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // Speed of movement
    public float speed = 2f;

    // Update is called once per frame
    void Update()
    {
        // Move the object to the right every frame
        transform.position += Vector3.right * speed * Time.deltaTime;
    }
}
