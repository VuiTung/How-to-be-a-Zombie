using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    void Start()
    {
        rb.velocity = transform.right * speed;
    }
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        HealthBar health = hitInfo.GetComponent<HealthBar>();
        if (health != null)
        {
            health.damage(5);
        }
        Destroy(gameObject);
    }

}
