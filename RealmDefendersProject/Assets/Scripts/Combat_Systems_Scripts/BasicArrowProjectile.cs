using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicArrowProjectile : MonoBehaviour
{
    public static BasicArrowProjectile Create(Vector3 poistion, Enemy enemy)
    {
        Transform pf_Basic_ArrowProjectile = Resources.Load<Transform>("pf_Basic_ArrowProjectile");
        Transform basicArrowProjectileTransform = Instantiate(pf_Basic_ArrowProjectile, poistion, Quaternion.identity);

        BasicArrowProjectile basicArrowProjectile = basicArrowProjectileTransform.GetComponent<BasicArrowProjectile>();
        basicArrowProjectile.SetTarget(enemy);
        return basicArrowProjectile;
    }

    private Enemy targetEnemy;
    public float damage;
    public float moveSpeed;
    private Vector3 lastMoveDirection;
    public float lifeSpawn = 2f;

    private void Update()
    {
        Vector3 moveDirection;
        if (targetEnemy != null)
        {
            moveDirection = (targetEnemy.transform.position - transform.position).normalized;
            lastMoveDirection = moveDirection;
        }
        else
        {
            moveDirection = lastMoveDirection;
        }

        transform.position += moveDirection * moveSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, 0, UtilitieClass.GetAngleFromVector(moveDirection));

        lifeSpawn -= Time.deltaTime;
        if (lifeSpawn <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private void SetTarget(Enemy targertEnemy)
    {
        this.targetEnemy = targertEnemy;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.GetComponent<HealthSystem>().Damage(damage);
            Destroy(gameObject);
        }
    }
}
