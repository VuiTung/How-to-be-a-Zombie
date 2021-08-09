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
    public float AttackRate = 20f;
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
        Debug.Log("Hit once");
        // play an attack animation
        //animator.SetTrigger("Attack"); // currently not working, maybe it's the animator didn't configure properly

        // detect enemy in range of attackss
        Collider2D[] hitenemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, enemyLayers); // this line get all the enemy in the attack range and store them into an array

        // damage them
        int size = hitenemies.Length; //get the array size of the hitenemies
        int counter = 0; // initialize the counter
        string[] check = new string[size]; // create another array with the size above
        for (int i = 0; i<size; i++){ //populate something to the array so it won't have error when comparing
            check[i] = "default value";
        }
            foreach(Collider2D enemy in hitenemies) { // for each enemy in the array hitenemies
            bool duplicate = false; //declare duplication checker
                for (int i = 0; i<size; i++) {
                    if (check[i].Equals(enemy.name)) { //check if their name is same
                        duplicate = true;
                        break;
                    } else { //damage the enemy if the loop has completed
                        duplicate = false;
                    } 
                }
                if (duplicate == false) {
                    enemy.GetComponent<Enemy>().takeDamage(damage); // call damage function in enemy script
                        check[counter] = enemy.name; //dump enemy into array
                        counter++;
                }      
        }
        
    }

    void OnDrawGizmosSelected() {
        if (AttackPoint == null){
            return;
        } 
        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }
}
