using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float MaxHP;
    float CurrentHP;
    void Start()
    {
        CurrentHP = MaxHP; // assign hp to max hp
    }

    public void takeDamage(float damage) {
            CurrentHP -= damage;
            Debug.Log("Bonked " + this.name + ", " + CurrentHP + " left");
            if (CurrentHP <= 0) {
            //Destroy(this);
            Debug.Log(this.name + " is dead");
            GetComponent < follow2 > ().enabled = true;
            GetComponent<Shooting>().enabled = false;
            gameObject.layer = LayerMask.NameToLayer("FU");
            transform.Rotate(0.0f, 180.0f, 0.0f);
            this.enabled = false;
        }
        }
}
