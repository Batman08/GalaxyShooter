using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher : MonoBehaviour
{
    public GameObject MissilePrefab;
    public Transform FirePoint;
    public Transform Target;
    public Transform PartToRotate;
    public float Range = 15f;
    public float TurnSpeed = 10f;
    public float FireRate = 1f;
    public string EnemyTag = "Enemy";

    private float FireCountDown = 0f;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void Update()
    {
        if (Target == null)
            return;

        Vector3 dir = Target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(PartToRotate.rotation, lookRotation, Time.deltaTime * TurnSpeed).eulerAngles;
        PartToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (FireCountDown <= 0f)
        {
            Shoot();
            FireCountDown = 1f / FireRate;
        }

        FireCountDown -= Time.deltaTime;
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(EnemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= Range)
        {
            Target = nearestEnemy.transform;
        }
    }

    void Shoot()
    {
        GameObject missileGo = Instantiate(MissilePrefab, FirePoint.position, FirePoint.rotation);
        Missile missile = missileGo.GetComponent<Missile>();

        if (missile != null)
            missile.Seek(Target);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}
