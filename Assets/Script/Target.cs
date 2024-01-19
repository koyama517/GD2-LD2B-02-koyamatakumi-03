using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float detectionRadius = 5f;
    public LayerMask enemyLayer;
    private Transform nearestEnemy;

    void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, enemyLayer);

        if (colliders.Length > 0)
        {
            nearestEnemy = GetNearestEnemy(colliders);
        }
        else
        {
            nearestEnemy = null;
        }
    }

    Transform GetNearestEnemy(Collider2D[] enemies)
    {
        Transform nearest = null;
        float minDistance = float.MaxValue;

        foreach (Collider2D enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);

            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = enemy.transform;
            }
        }

        return nearest;
    }

    public Transform GetNearestEnemy()
    {
        return nearestEnemy;
    }
}
