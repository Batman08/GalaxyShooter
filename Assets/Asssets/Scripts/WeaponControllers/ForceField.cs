using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour
{
    public static ForceField instnace;

    public int Health;

    private int _startHealth = 10;

    void OnEnable()
    {
        instnace = this;
        Health = _startHealth;
    }

    void Update()
    {
        CheckHealth();
    }

    void CheckHealth()
    {
        bool NoHealth = (Health <= 0);
        if (NoHealth)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        bool Enemy = (collision.collider.CompareTag("Enemy"));
        bool EnemyBolt = (collision.collider.CompareTag("EnemyBolt"));

        if (Enemy || EnemyBolt)
        {
            Destroy(collision.collider.gameObject);
        }
    }
}
