using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bridge : MonoBehaviour
{
    public Transform Mc;
    public Component bridge;
    private GameObject[] objs;
    public int requiredZombie =5;

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(Mc.position, transform.position) < 2 && (int)Math.Floor(Mc.position.y) == (int)Math.Floor(transform.position.y))
        {

            if (bridge.GetComponent<Rigidbody2D>().simulated == false) {

                if (Input.GetKeyDown(KeyCode.V))
                {
                    int numberOfZombie = 0;
                    objs = GameObject.FindGameObjectsWithTag("FriendlyUnit");
                    foreach (GameObject obj in objs)
                    {
                        numberOfZombie++;
                        if (numberOfZombie == requiredZombie)
                        {
                            break;
                        }
                    }
                    int sacrifiedZombie = 0;
                    if (numberOfZombie >= requiredZombie)
                    {
                        foreach (GameObject obj in objs)
                        {
                            if (sacrifiedZombie == requiredZombie)
                            {
                                break;
                            }
                            Vector3 myVector = new Vector3(transform.position.x, obj.transform.position.y, transform.position.z);
                            follow2 follow2 = obj.GetComponent<follow2>();

                            follow2.SetDestination(myVector, 1);

                            sacrifiedZombie++;

                        }
                        bridge.GetComponent<Rigidbody2D>().simulated = true;
                    }
                    else
                    {
                        Debug.Log("not enough Zombie");
                    }
                    
                }
            }
        }
        else
        {
        }
    }
}
