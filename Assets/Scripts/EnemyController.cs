using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    public float health = 100;
    public Image healthBar;
    private bool isBurning = false;
    private float burnDamage = 0; 
    private float burnDuration = 0;
    public static event System.Action<Transform> OnEnemyDied;

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.fillAmount = health / 100f;

        if (health <= 0)
        {
            Die();
        }
    }

    public void ApplyBurnDamage(int damage, float duration)
    {
        if (!isBurning)
        {
            isBurning = true;
            burnDamage = damage;
            burnDuration = duration;
            StartCoroutine(Burn());
        }
    }

    IEnumerator Burn()
    {
        float timer = 0f;
        while (timer < burnDuration)
        {
            TakeDamage(burnDamage); 
            timer += 1f; 
            yield return new WaitForSeconds(1f);
        }
        isBurning = false;
    }

    private void Die()
    {
        // Enemy öldüğünde olayı tetikle
        OnEnemyDied?.Invoke(transform);

        // Yeniden doğur
        Respawn();
    }

    public void Respawn()
    {
        float z = Random.Range(-2.5f, 11.5f);
        float x = Random.Range(-4f, 4f);
        transform.position = new Vector3(x, transform.position.y, z);
        health = 100;
        healthBar.fillAmount = 1f;
    }
}