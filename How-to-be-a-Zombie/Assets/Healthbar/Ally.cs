using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : MonoBehaviour
{
    public AllyHp allyhp;
    public float MaxHP;
    float CurrentHP;
    
    void Start()
    {
        CurrentHP = MaxHP; // assign hp to max hp
        allyhp.SetHealth(CurrentHP, MaxHP); //initialize the hp bar

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(float damage) {
            CurrentHP -= damage; // deduct enemy hp
            allyhp.SetHealth(CurrentHP, MaxHP); //update enemy hp bar
            Debug.Log("Bonked " + this.name + ", " + CurrentHP + " left");
            if (CurrentHP <= 0) {
            //deactivate this script and activate another scrpit
            Debug.Log(this.name + " is dead");
}
    }
}
