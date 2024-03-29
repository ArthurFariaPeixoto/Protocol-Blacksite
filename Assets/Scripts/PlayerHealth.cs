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
        armor = maxArmor;
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
        if (armor > 0)
        {
            if (armor >= damage)
            {
                armor -= damage;
            }
            else if (armor < damage)
            {
                int remainingDamage;
                remainingDamage = damage - armor;
                armor = 0;
                health -= remainingDamage;
            }
        }
        else
        {
            health -= damage;
        }

        if (health <= 0)
        {
            health = 0;
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.buildIndex);
        }

    }
}
