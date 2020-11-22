using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Enemy Create(Vector3 poistion)
    {
        Transform pf_Enemy = Resources.Load<Transform>("pf_Enemy");
        Transform enemyTransform = Instantiate(pf_Enemy, poistion, Quaternion.identity);

        Enemy enemy = enemyTransform.GetComponent<Enemy>();
        return enemy;
    }

    private HealthSystem healthSystem;
    private Transform targetTransform;  // Transform of target
    private Rigidbody2D rigidbody2D;    //rigidbody referance of this enemy
    private float lookForTargetTimer;
    private float lookForTargetTimerMax = 0.2f;    // How often should enemy look for a target

    public float moveSpeed; // Speed of enemy
    public float damage;    // Damage done by enemy

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        targetTransform = BuildingManager.Instance.GetPlayerCampBuilding().transform;
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnDied += HealthSystem_OnDied;

        lookForTargetTimer = UnityEngine.Random.Range(0f, lookForTargetTimerMax);
    }

    private void HealthSystem_OnDied(object sender, EventArgs e)
    {
        // When Health reaches zero 
        Destroy(gameObject);
    }

    private void Update()
    {
        Movement();
        Target();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Building building = collision.gameObject.GetComponent<Building>();
        if (building != null)
        {
            // Enemy Collided with a building
            HealthSystem healthSystem = building.GetComponent<HealthSystem>();
            healthSystem.Damage(damage);
            Destroy(gameObject);
        }
    }

    private void Movement()
    {
        if (targetTransform != null)
        {
            Vector3 moveDirection = (targetTransform.position - transform.position).normalized;
            rigidbody2D.velocity = moveDirection * moveSpeed;
        }
        else
        {
            if (lookForTargetTimer <= 0)
            {
                Destroy(gameObject);
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

    private void LookForTargets()
    {
        float targetMaxRadius = 10f;
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);

        foreach (Collider2D collider2D in collider2DArray)
        {
            Building building = collider2D.GetComponent<Building>();
            if (building != null)
            {
                if (targetTransform == null)
                {
                    targetTransform = building.transform;
                }
                else
                {
                    if (Vector3.Distance(transform.position, building.transform.position) < Vector3.Distance(transform.position, targetTransform.position))
                    {
                        // Closest Target
                        targetTransform = building.transform;
                    }
                }
            }
        }

        if (targetTransform == null)
        {
            // No targets in range
            targetTransform = BuildingManager.Instance.GetPlayerCampBuilding().transform;
        }
    }
}
