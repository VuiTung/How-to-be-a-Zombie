using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButton(0)==true) {
            NormalHit();
        }
    }

    void NormalHit() {
        // play an attack animation
        // detect enemy in range of attacks
        // damage them
    }
}
