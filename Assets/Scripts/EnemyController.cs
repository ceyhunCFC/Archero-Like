using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class EnemyController : MonoBehaviour
{
    public float health = 100;
    public Image healthBar;
    private bool isBurning = false; // Yanık hasarı aktif mi?
    private float burnDamage = 0; // Yanık hasarı miktarı
    private float burnDuration = 0; // Yanık süresi

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.fillAmount = health / 100f;

        if (health <= 0)
        {
            Respawn();
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
            TakeDamage(burnDamage); // Yanık hasarı uygula
            timer += 1f; // Her saniye hasar ver
            yield return new WaitForSeconds(1f);
        }
        isBurning = false;
    }

    void Respawn()
    {
        float z = Random.Range(-2.5f, 11.5f);
        float x = Random.Range(-4f, 4f);
        transform.position = new Vector3(x, transform.position.y, z);
        health = 100;
        healthBar.fillAmount = 1f;
    }
}