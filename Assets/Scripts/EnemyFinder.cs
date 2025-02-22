using UnityEngine;

public class EnemyFinder : MonoBehaviour
{
    private GameObject nearestEnemy; 
    public float detectionRange = 10f; 
    [HideInInspector] public Transform targetEnemy; 
    private CharacterController controller;  

    void Start()
    {
        controller = GetComponent<CharacterController>();
        EnemyController.OnEnemyDied += HandleEnemyDied;
    }

    void OnDestroy()
    {
        EnemyController.OnEnemyDied -= HandleEnemyDied;
    }

    void HandleEnemyDied(Transform enemyTransform)
    {
        if (targetEnemy == enemyTransform)
        {
            targetEnemy = null;
        }
    }

    void Update()
    {
        if (targetEnemy != null)
        {
            LookAtEnemy();  
        }
        else if (controller.state == 0)
        {
            FindNearestEnemy();
        }
    }

    void FindNearestEnemy()
    {
        nearestEnemy = null;
        float minDistance = detectionRange;

        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, detectionRange);

        foreach (Collider enemy in enemiesInRange)
        {
            if (enemy.CompareTag("Enemy"))
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

                if (distanceToEnemy < minDistance)
                {
                    nearestEnemy = enemy.gameObject;
                    targetEnemy = enemy.transform;
                    minDistance = distanceToEnemy;  
                }
            }
        }
    }

    void LookAtEnemy()
    {
        controller.state = 2;
        Vector3 directionToEnemy = targetEnemy.position - transform.position;
        directionToEnemy.y = 0;  
        Quaternion lookRotation = Quaternion.LookRotation(directionToEnemy);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}