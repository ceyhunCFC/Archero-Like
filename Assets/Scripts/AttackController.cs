using System.Collections;
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
        if (enemyFinder.targetEnemy == null)
        return;

        switch (SkillManager.activeSkill)
        {
            case SkillType.ArrowMultiplication:
                Debug.Log("Arrow Multiplication skill activated!");
                ArrowMultiplicationAttack();
                break;

            case SkillType.BounceDamage:
                Debug.Log("Bounce Damage skill activated!");
                BounceDamageAttack();
                break;

            case SkillType.BurnDamage:
                Debug.Log("Burn Damage skill activated!");
                BurnRegularAttack();
                break;

            case SkillType.AttackSpeedIncrease:
                Debug.Log("Attack Speed Increase skill activated!");
                RegularAttack(true);
                break;

            case SkillType.RageMode:
                Debug.Log("Rage Mode skill activated!");
                RageModeAttack();
                break;

            default:
                Debug.Log("No skill active.");
                RegularAttack(false);
                break;
        }
    }

    void RegularAttack(bool Speedup)
    {
        Animator anim = GetComponent<Animator>();
        if (Speedup)
        anim.speed = 2f; 
        else
        anim.speed = 1f; 
        
        
        GameObject arrow = Instantiate(arrowPrefab,new Vector3(transform.position.x,transform.position.y+0.5f,transform.position.z), Quaternion.identity);
        Rigidbody arrowRb = arrow.GetComponent<Rigidbody>();
        ArrowRotation arrowRotation = arrow.GetComponent<ArrowRotation>();
        
        if (arrowRotation != null)
        {
            arrowRotation.SetBounceDamage(false);
        }

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
    }

     void BurnRegularAttack()
    {
          Animator anim = GetComponent<Animator>();
        if (anim != null)
        {
            anim.speed = 1f; 
        }

        GameObject arrow = Instantiate(arrowPrefab,new Vector3(transform.position.x,transform.position.y+0.5f,transform.position.z), Quaternion.identity);
        Rigidbody arrowRb = arrow.GetComponent<Rigidbody>();
        ArrowRotation arrowRotation = arrow.GetComponent<ArrowRotation>();
        
        if (arrowRotation != null)
        {
            arrowRotation.SetBurnDamage(true);
        }

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
    }

    void ArrowMultiplicationAttack()
    {
          Animator anim = GetComponent<Animator>();
        if (anim != null)
        {
            anim.speed = 1f; 
        }

        for (int i = 0; i < 2; i++) // 2 ok fırlat
        {
            GameObject arrow = Instantiate(arrowPrefab, new Vector3(transform.position.x + (i * 0.4f), transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
            Rigidbody arrowRb = arrow.GetComponent<Rigidbody>();

            ArrowRotation arrowRotation = arrow.GetComponent<ArrowRotation>();
            if (arrowRotation != null)
            {
                arrowRotation.SetBounceDamage(false);
            }
            
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
        }
    }

    void BounceDamageAttack()
    {
         Animator anim = GetComponent<Animator>();
        if (anim != null)
        {
            anim.speed = 1f; 
        }

        GameObject arrow = Instantiate(arrowPrefab, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
        Rigidbody arrowRb = arrow.GetComponent<Rigidbody>();
        
        ArrowRotation arrowRotation = arrow.GetComponent<ArrowRotation>();
        if (arrowRotation != null)
        {
            arrowRotation.SetBounceDamage(true);
        }

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
    }
    

    void RageModeAttack()
    {
        // Tüm skill'lerin etkisini iki katına çıkar
        Debug.Log("Rage Mode activated! All skills are doubled.");
        
        Animator anim = GetComponent<Animator>();
        if (anim != null)
        {
            anim.speed = 2f; 
        }

        // Örnek: Ok çoğaltma skill'i 4 ok fırlatsın
        for (int i = 0; i < 2; i++)
        {
            GameObject arrow = Instantiate(arrowPrefab, new Vector3(transform.position.x + (i * 0.4f), transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
            Rigidbody arrowRb = arrow.GetComponent<Rigidbody>();
            
            ArrowRotation arrowRotation = arrow.GetComponent<ArrowRotation>();
            if (arrowRotation != null)
            {
                arrowRotation.SetBounceDamage(true);
                arrowRotation.SetBurnDamage(true);
            }

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
        }
    }

    private Vector3 CalculateArcVelocity(Vector3 start, Vector3 end, float arcHeight)
    {
        Vector3 displacement = end - start;
        float horizontalDistance = new Vector3(displacement.x, 5, displacement.z).magnitude;
        float verticalDistance = displacement.y;

        float gravity = Physics.gravity.magnitude;
        float time = Mathf.Sqrt(2 * arcHeight / gravity) + Mathf.Sqrt(2 * (arcHeight + verticalDistance) / gravity);

        Vector3 horizontalVelocity = new Vector3(displacement.x, 0, displacement.z).normalized * (horizontalDistance / time);
        Vector3 verticalVelocity = Vector3.up * Mathf.Sqrt(2 * gravity * arcHeight);

        return horizontalVelocity + verticalVelocity;
    }
}
