using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFinder : MonoBehaviour
{
    private GameObject nearestEnemy;  // En yakın düşmanı tutacak değişken
    public float detectionRange = 10f;  // Düşmanları tespit etme mesafesi
    [HideInInspector] public Transform targetEnemy;  // En yakın düşmanın transform'u
    private CharacterController controller;  // Oyuncunun karakter kontrolcü'sü
    
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    
    void Update()
    {
       print (controller.state );
       
        if (targetEnemy != null)
        {
            
            LookAtEnemy();  // Düşmanı bulduğunda ona bak
        }
        else if (controller.state == 0)
        FindNearestEnemy();  // Her frame en yakın düşmanı bul
    }

    void FindNearestEnemy()
    {
        // Nearest enemy ve minimum mesafeyi başlat
        nearestEnemy = null;
        float minDistance = detectionRange;

        // Tüm düşmanları bulmak için bir Collider sorgusu kullanabiliriz (örneğin, tag ile)
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, detectionRange);

        foreach (Collider enemy in enemiesInRange)
        {
            // "Enemy" etiketiyle işaretlenmiş nesneleri kontrol et
            if (enemy.CompareTag("Enemy"))
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                
                // Eğer bu düşman daha yakınsa, onu en yakın düşman olarak kaydet
                if (distanceToEnemy < minDistance)
                {
                    nearestEnemy = enemy.gameObject;
                    targetEnemy = enemy.transform;
                    minDistance = distanceToEnemy;  // Yeni minimum mesafeyi güncelle
                }
            }
        }
    }

    void LookAtEnemy()
    {
        controller.state = 2;
        // Karakterin düşmana dönmesi için
        Vector3 directionToEnemy = targetEnemy.position - transform.position;
        directionToEnemy.y = 0;  // Y eksenindeki farkı sıfırla, sadece yatayda bakmasını sağla
        Quaternion lookRotation = Quaternion.LookRotation(directionToEnemy);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);  // Yavaşça dönmesini sağla
    }
}
