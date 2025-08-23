using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public class Bullet : MonoBehaviour
{
    private float speed = 3.5f;
    public int damage = 10;
    private Transform monster;
    private Vector3 monsterPosition;
    private Vector3 direction;
    void Start()
    {
        monster = GameObject.FindGameObjectsWithTag("Monster").FirstOrDefault().transform;
        if (monster != null)
            monsterPosition = monster.transform.position;
    }
    void Update()
    {
        if (monster != null)
        {
            direction = (monsterPosition - transform.position).normalized;
        }
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
