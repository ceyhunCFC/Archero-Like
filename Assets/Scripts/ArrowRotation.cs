using System.Collections;
using UnityEngine;

public class ArrowRotation : MonoBehaviour
{
    private Rigidbody rb;
    private bool hasHit = false;
    private bool hasBounced = false; // Sekme özelliği aktif mi?
    private int bounceCount = 0; // Kaç kez sekti?
    private int maxBounces = 1; // Maksimum sekme sayısı (1 kez sekebilir)
    private float bounceRange = 50f; // Sekme mesafesi
    private Transform lastHitEnemy; // Son hasar verilen düşman

    public void SetBounceDamage(bool enable)
    {
        hasBounced = enable; // Sekme özelliğini aktif et
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!hasHit && rb != null && rb.velocity != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (hasHit) return;

        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemy = other.gameObject.GetComponent<EnemyController>();
            if (enemy != null && enemy.transform != lastHitEnemy) // Aynı düşmana tekrar hasar verme
            {
                enemy.TakeDamage(30); // Düşmana hasar ver
                lastHitEnemy = enemy.transform; // Son hasar verilen düşmanı kaydet
            }

            // Sekme özelliği aktifse ve maksimum sekme sayısına ulaşılmadıysa
            if (hasBounced && bounceCount < maxBounces)
            {
                Transform nearestEnemy = FindNearestEnemy(other.transform);
                if (nearestEnemy != null)
                {
                    BounceToEnemy(nearestEnemy);
                    bounceCount++;
                    return; // Sekme işlemi başladı, çarpışmayı sonlandır
                }
            }

            // Sekme yoksa veya sekme tamamlandıysa
            StickToTarget(other.transform);
            StartCoroutine(DelayForDestroy());
        }
        else if (!other.gameObject.CompareTag("Character"))
        {
            Destroy(this.gameObject);
        }
    }

    void StickToTarget(Transform target)
    {
        transform.SetParent(target);
        rb.isKinematic = true;
        GetComponent<Collider>().enabled = false;
        hasHit = true;
    }

    void BounceToEnemy(Transform enemy)
    {
        if (enemy == null) return; // Hedef yoksa işlemi sonlandır

        // Okun mevcut pozisyonu ve hedef pozisyonu
        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = enemy.position + Vector3.up * 1f; // Hedefin biraz yukarısına git

        // Yeni hız vektörünü hesapla (fiziksel yörünge ile)
        Vector3 initialVelocity = CalculateArcVelocity(currentPosition, targetPosition, arcHeight: 2f);

        // Yeni hız vektörünü ayarla
        rb.velocity = initialVelocity;

        // Okun rotasyonunu güncelle
        transform.rotation = Quaternion.LookRotation(initialVelocity);
    }

    Vector3 CalculateArcVelocity(Vector3 start, Vector3 end, float arcHeight)
    {
        Vector3 displacement = end - start;
        float horizontalDistance = new Vector3(displacement.x, 0, displacement.z).magnitude;
        float verticalDistance = displacement.y;

        float gravity = Physics.gravity.magnitude;
        float time = Mathf.Sqrt(2 * arcHeight / gravity) + Mathf.Sqrt(2 * (arcHeight + verticalDistance) / gravity);

        Vector3 horizontalVelocity = new Vector3(displacement.x, 0, displacement.z).normalized * (horizontalDistance / time);
        Vector3 verticalVelocity = Vector3.up * Mathf.Sqrt(2 * gravity * arcHeight);

        return horizontalVelocity + verticalVelocity;
    }

    Transform FindNearestEnemy(Transform currentEnemy)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform nearestEnemy = null;
        float minDistance = bounceRange;

        foreach (GameObject enemy in enemies)
        {
            if (enemy.transform != currentEnemy && enemy.transform != lastHitEnemy) // Mevcut ve son hasar verilen düşmanı hariç tut
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestEnemy = enemy.transform;
                }
            }
        }
        return nearestEnemy;
    }

    IEnumerator DelayForDestroy()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}