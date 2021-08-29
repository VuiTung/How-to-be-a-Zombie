using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public float damage;
    void Start()
    {
        rb.velocity = transform.right * speed;
    }
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        HealthBar health = hitInfo.GetComponent<HealthBar>();
        Ally Ally = hitInfo.GetComponent<Ally>();
        if (health != null)
        {
            health.damage(damage);
        }
        else if(Ally != null){
            Ally.takeDamage(damage);
        }
        Destroy(gameObject);
    }

}
