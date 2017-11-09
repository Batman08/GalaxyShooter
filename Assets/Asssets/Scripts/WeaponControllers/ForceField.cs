using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour
{
    public int Health;

    private int _startHealth = 10;
    void Start()
    {
        Health = _startHealth;
    }

    void Update()
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
            Health--;
            Destroy(collision.collider.gameObject);
        }
    }
}
