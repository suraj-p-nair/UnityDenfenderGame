using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public class Bullet : Projectiles
{
    void Start()
    {
        var closestMonster = MonsterFactory.Instance.GetClosestMonster(transform.position);
        if (closestMonster != null)
        {
            direction = (closestMonster.position - transform.position).normalized;
        }
        Destroy(gameObject, 4f);
    }
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
