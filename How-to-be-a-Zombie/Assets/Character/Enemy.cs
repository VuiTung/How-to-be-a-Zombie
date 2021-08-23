using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyHp enemyHp;
    public float MaxHP;
    float CurrentHP;
    
    void Start()
    {
        CurrentHP = MaxHP; // assign hp to max hp
        enemyHp.SetHealth(CurrentHP, MaxHP); //initialize the hp bar

    }

    public void takeDamage(float damage) {
            CurrentHP -= damage; // deduct enemy hp
            enemyHp.SetHealth(CurrentHP, MaxHP); //update enemy hp bar
            Debug.Log("Bonked " + this.name + ", " + CurrentHP + " left");
            if (CurrentHP <= 0) {
            //deactivate this script and activate another scrpit
            Debug.Log(this.name + " is dead");
            GetComponent < follow2 > ().enabled = true;
            GetComponent<Shooting>().enabled = false;
            gameObject.layer = LayerMask.NameToLayer("FU");
            transform.Rotate(0.0f, 180.0f, 0.0f);
            this.enabled = false;
        }
        }
}
