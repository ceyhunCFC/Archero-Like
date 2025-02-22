using System.Collections;
using UnityEngine;

public class ArrowRotation : MonoBehaviour
{
    private Rigidbody rb;
    private bool hasHit = false; 

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
            transform.SetParent(other.transform);
            
            if (enemy != null)
            {
                enemy.TakeDamage(30);
            }

            rb.isKinematic = true;
            GetComponent<Collider>().enabled = false;
            hasHit = true;

            StartCoroutine(DelayForDestroy());
        }
        else if (!other.gameObject.CompareTag("Character"))
        {
            print(other.gameObject.name);
            Destroy(this.gameObject);
        }
    }

    IEnumerator DelayForDestroy()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}