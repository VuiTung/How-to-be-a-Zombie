using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    //public Animator animator;
    public Transform AttackPoint;
    public LayerMask enemyLayers;
    public float AttackRange = 0.5f;
    public float damage = 5f;
    public float AttackRate = 1f;
    float nextAttackTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (Time.time >= nextAttackTime) {
        if (Input.GetMouseButton(0)==true) {
            NormalHit();
            nextAttackTime =  Time.time + 1f / AttackRate;
        }
        }
    }

    void NormalHit() {
        // play an attack animation
        //animator.SetTrigger("Attack"); // currently not working, maybe it's the animator didn't configure properly

        // detect enemy in range of attacks
        Collider2D[] hitenemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, enemyLayers); // this line get all the enemy in the attack range and store them into an array

        
        // damage them
        foreach(Collider2D enemy in hitenemies) { // for each enemy in the array
           enemy.GetComponent<Enemy>().takeDamage(damage);
        }
    }

    void OnDrawGizmosSelected() {
        if (AttackPoint == null){
            return;
        } 
        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }
}
