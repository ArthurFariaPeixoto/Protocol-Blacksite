using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    private int health;

    public int maxArmor;
    private int armor;

    void Start()
    {
        health = maxHealth;
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            DamagePlayer(30);
        }
    }


    public void DamagePlayer(int damage)
    {
        int remainingDamage = damage;
        if (armor > 0)
        {
            if (armor >= damage)
            {
                armor -= damage;
                remainingDamage = 0;
            }
            else
            {
                remainingDamage -= armor;
                armor = 0;
            }
        }
        health -= remainingDamage;
        health = Mathf.Max(0, health); // Garante que a saúde não seja negativa
        if (health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload scene directly
        }
    }

    public void GiveHealth(int amount, GameObject pickup)
    {
        if (health < maxHealth)
        {
            health += amount;
            Destroy(pickup);
        }
        if (health > maxHealth)
        {
            health = maxHealth;
        }

    }
    public void GiveArmor(int amount, GameObject pickup)
    {

        if (armor < maxArmor)
        {
            armor += amount;
            Destroy(pickup);
        }
        if (armor > maxArmor)
        {
            armor = maxArmor;
        }
    }

}
