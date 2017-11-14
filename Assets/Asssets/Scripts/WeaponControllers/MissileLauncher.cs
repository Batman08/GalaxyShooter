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

    [Header("Use Laser")]
    public bool UseLaser = false;
    public LineRenderer LineRenderer;

    private float FireCountDown = 0f;

    void Start()
    {
        UseLaser = true;
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void Update()
    {
        if (Target == null)
        {
            if (UseLaser)
            {
                if (LineRenderer.enabled)
                    LineRenderer.enabled = false;
            }

            return;
        }

        LockOnTarget();

        if (UseLaser)
        {
            Laser();
        }

        else
        {
            if (FireCountDown <= 0f)
            {
                Shoot();
                FireCountDown = 1f / FireRate;
            }
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

    void LockOnTarget()
    {
        Vector3 dir = Target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(PartToRotate.rotation, lookRotation, Time.deltaTime * TurnSpeed).eulerAngles;
        PartToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Laser()
    {
        if (!LineRenderer.enabled)
        {
            LineRenderer.enabled = true;
        }

        LineRenderer.SetPosition(0, FirePoint.position);
        LineRenderer.SetPosition(1, Target.position);
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
