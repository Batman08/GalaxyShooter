using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public GameObject ImpactEffect;
    public float Speed = 30f;
    public float ExplosionRadius = 0f;

    public Transform target;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisfame = Speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisfame)
        {
            HitTarget();
            GameManager.instance.AddScore(150);
            return;
        }

        transform.Translate(dir.normalized * distanceThisfame, Space.World);
        transform.LookAt(target);
    }

    void HitTarget()
    {
        Instantiate(ImpactEffect, transform.position, transform.rotation);
        if (ExplosionRadius > 0f)
        {
            Explode();
        }

        else
        {
            DestroyEnemy(target);
        }

        Destroy(gameObject);
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, ExplosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                //Physics.IgnoreLayerCollision(8, 10);
                DestroyEnemy(collider.transform);
            }
        }
    }

    void DestroyEnemy(Transform enemy)
    {
        Destroy(enemy.gameObject);
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ExplosionRadius);
    }
}
