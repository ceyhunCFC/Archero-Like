using UnityEngine;

public class AttackController : MonoBehaviour
{
    private EnemyFinder enemyFinder;
    public GameObject arrowPrefab; 
    public float arrowSpeed = 10f; 
    public float arcHeight = 2f;
    public float closeRangeThreshold = 5f;

    void Start()
    {
        enemyFinder = GetComponent<EnemyFinder>();
    }

    public void Attack()
    {
        if (enemyFinder.targetEnemy != null)
        {
            GameObject arrow = Instantiate(arrowPrefab,new Vector3(transform.position.x,transform.position.y+0.5f,transform.position.z), Quaternion.identity);
            Rigidbody arrowRb = arrow.GetComponent<Rigidbody>();

            if (arrowRb != null)
            {
                Vector3 targetPosition = enemyFinder.targetEnemy.transform.position + Vector3.up * 1f;
                Vector3 direction = (targetPosition - transform.position).normalized;

                float distanceToTarget = Vector3.Distance(transform.position, targetPosition);

                if (distanceToTarget <= closeRangeThreshold)
                {
                    arrowRb.velocity = direction * arrowSpeed;
                }
                else
                {
                    Vector3 initialVelocity = CalculateArcVelocity(transform.position, targetPosition, arcHeight);
                    arrowRb.velocity = initialVelocity;
                }

            }
            else
            {
                Debug.LogError("Arrow prefab does not have a Rigidbody component.");
            }
        }
        else
        {
            Debug.LogWarning("No target enemy found.");
        }
    }

    private Vector3 CalculateArcVelocity(Vector3 start, Vector3 end, float arcHeight)
    {
        Vector3 displacement = end - start;
        float horizontalDistance = new Vector3(displacement.x, 6, displacement.z).magnitude;
        float verticalDistance = displacement.y;

        float gravity = Physics.gravity.magnitude;
        float time = Mathf.Sqrt(2 * arcHeight / gravity) + Mathf.Sqrt(2 * (arcHeight + verticalDistance) / gravity);

        Vector3 horizontalVelocity = new Vector3(displacement.x, 0, displacement.z).normalized * (horizontalDistance / time);
        Vector3 verticalVelocity = Vector3.up * Mathf.Sqrt(2 * gravity * arcHeight);

        return horizontalVelocity + verticalVelocity;
    }
}
