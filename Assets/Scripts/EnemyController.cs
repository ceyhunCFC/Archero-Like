using System;
using UnityEngine;
using UnityEngine.UI;
public class EnemyController : MonoBehaviour
{
    public static event Action<Transform> OnEnemyDied; 

    int health = 100; 
    public Image healthBar; 

    void Start()
    {
        UpdateHealthBar();  
    }

    public void TakeDamage(int damage)
    {
        health -= damage; 
        health = Mathf.Clamp(health, 0, 100);  

        if (health <= 0)
        {
            RemoveAllArrow();
           
          
            OnEnemyDied?.Invoke(transform);
        }

        UpdateHealthBar();  
    }

    void UpdateHealthBar()
    {
        healthBar.fillAmount = health / 100f; 
    }

    void RespawnEnemy()
    {
        float z = UnityEngine.Random.Range(-2.5f, 11.5f);
        float x = UnityEngine.Random.Range(-4f, 4f);
        transform.position = new Vector3(x, transform.position.y, z);
        health = 100;
        UpdateHealthBar();
    }

    void RemoveAllArrow()
    {
        Transform[] arrows = transform.GetComponentsInChildren<Transform>();

        foreach (var arrow in arrows)
        {
            if (arrow.name == "Arrow(Clone)")
            {
                Destroy(arrow.gameObject);
            }
        }

        RespawnEnemy();
    }
}