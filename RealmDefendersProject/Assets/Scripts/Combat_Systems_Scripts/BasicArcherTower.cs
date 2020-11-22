using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicArcherTower : MonoBehaviour
{
    private Enemy targetEnemy;
    private Vector3 projectileSpawnPoint;

    private float fireAtTargetTimer;
    [SerializeField] private float fireAtTargertTimerMax = 0.2f;    // How often should tower fire at a target

    private float lookForTargetTimer;
    private float lookForTargetTimerMax = 0.2f;    // How often should tower look for a target

    private void Awake()
    {
        projectileSpawnPoint = transform.Find("projectileSpawnPoint").position;
    }

    private void Update()
    {
        Target();
        FireProjectile();

    }

    private void LookForTargets()
    {
        float targetMaxRadius = 20f;
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);

        foreach (Collider2D collider2D in collider2DArray)
        {
            Enemy enemy = collider2D.GetComponent<Enemy>();
            if (enemy != null)
            {
                if (targetEnemy == null)
                {
                    targetEnemy = enemy;
                }
                else
                {
                    if (Vector3.Distance(transform.position, enemy.transform.position) 
                        < Vector3.Distance(transform.position, targetEnemy.transform.position))
                    {
                        // Closest Target
                        targetEnemy = enemy;
                    }
                }
            }
        }
    }

    private void Target()
    {
        lookForTargetTimer -= Time.deltaTime;
        if (lookForTargetTimer <= 0)
        {
            LookForTargets();
            lookForTargetTimer += lookForTargetTimerMax;
        }
    }

    private void FireProjectile()
    {
        fireAtTargetTimer -= Time.deltaTime;
        if (fireAtTargetTimer <= 0f)
        {
            fireAtTargetTimer += fireAtTargertTimerMax;
            if (targetEnemy != null)
            {
                BasicArrowProjectile.Create(projectileSpawnPoint, targetEnemy);
            }
            else
            {

            }
        }
    }
}
