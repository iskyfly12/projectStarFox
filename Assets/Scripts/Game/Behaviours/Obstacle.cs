using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Range(0, 100)]
    [SerializeField] private int damage;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out IDamagable dmg))
            dmg.SetDamage(damage);
    }
}
