using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public class Bullet : MonoBehaviour
{
    private float speed = 3f;
    //private int damage = 10;
    private GameObject monster;
    private Vector3 monsterPosition;
    void Start()
    {
        monster = GameObject.FindGameObjectsWithTag("Monster").FirstOrDefault();
        if (monster != null)
            monsterPosition = monster.transform.position;
        Destroy(gameObject, 2f);
    }
    void Update()
    {
        if (monster == null)
        {
            return;
        }
        Vector3 direction = (monsterPosition - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
