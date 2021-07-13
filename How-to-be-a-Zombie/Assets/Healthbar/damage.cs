using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damage : MonoBehaviour
{
    public HealthBar health;

    public float dmg = 5f;

    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Player")) {
            Debug.Log("Hit");
            health.damage(dmg);
            // gameObject.GetComponent<newHealth>().damage(damage);
        }
    }

    void Update()
    {
        
    }
}
