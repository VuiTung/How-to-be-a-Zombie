using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBar;

    float health, maxhealth = 100f;
    float lerpSpeed;

    private void Start() {
        health = maxhealth;
        Healthbarfiller();
        ColourChanger();
    }

    private void Update() {
    }

    void Healthbarfiller() {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount , health/maxhealth, lerpSpeed);
    }

    void ColourChanger() {
        Color healthColour;// = Color.Lerp(Color.red, Color.green, (health/maxhealth));
        if (health/maxhealth < .5) { 
        healthColour = Color.Lerp (Color.red, Color.yellow, health/maxhealth);
        } else {
        healthColour = Color.Lerp (Color.yellow, Color.green, health/maxhealth);
        }
        healthBar.color = healthColour;
    }
    
    public void damage(float damage) {
        if (health > 0) {
            lerpSpeed = 3f * Time.deltaTime;
            health -= damage;
            Healthbarfiller();
            ColourChanger();
        }
    }

    public void heal(float heal) {
        if (health > 0) {
            lerpSpeed = 3f * Time.deltaTime;
            health += heal;
            Healthbarfiller();
            ColourChanger();
        }
    }

}
