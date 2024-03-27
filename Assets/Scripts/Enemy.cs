using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyManager enemyManager;
    private float enemyHealth = 5f;

    public GameObject gunHitObject;

    void Start()
    {

    }

    void Update()
    {
        //verifica se vida do inimigo e 0 ou menor
        if (enemyHealth <= 0)
        {
            //remove objeto da lista de inimigos em range
            enemyManager.removeEnemy(this);

            //destroi objeto inimigo
            Destroy(gameObject);
        }
    }

    //aplica dano ao inimigo
    public void TakeDamage(float damage)
    {
        Instantiate(gunHitObject, transform.position, Quaternion.identity);
        enemyHealth -= damage;
    }
}
