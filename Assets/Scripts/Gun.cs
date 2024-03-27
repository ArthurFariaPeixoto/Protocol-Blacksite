using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    //Alcance horizontal da arma
    public float range = 20f;

    //Alcance vertical da arma
    public float verticalRange = 20f;

    // fire rate kkkkkkkk
    public float fireRate;

    public float damage = 1f;

    // variavel para evitar spam de tiros
    private float nextTimeToFire;

    //Box Collider da arma para verificar se existe algum inimigo no range
    private BoxCollider gunTrigger;

    public LayerMask raycastLayerMask;
    public EnemyManager enemyManager;

    void Start()
    {
        //Inicia o box collider e o posiciona
        gunTrigger = GetComponent<BoxCollider>();
        gunTrigger.size = new Vector3(1, verticalRange, range);
        gunTrigger.center = new Vector3(0, 0, range * 0.5f);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time > nextTimeToFire)
        {
            Fire();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.transform.GetComponent<Enemy>();

        if (enemy)
        {
            //adicione o inimigo a  uma lista de inimigos em range
            enemyManager.addEnemy(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Enemy enemy = other.transform.GetComponent<Enemy>();

        if (enemy)
        {
            //remove o inimigo da lista de inimigos em range
            enemyManager.removeEnemy(enemy);
        }
    }

    private void Fire()
    {
        //Som para tiro
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().Play();

        //aplica dano em todos os inimigos em range
        foreach (var enemy in enemyManager.enemiesInRange)
        {
            //RayCast para evitar de tiros passarem por paredes
            var dir = enemy.transform.position - transform.position;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, dir, out hit, range * 1.5f, raycastLayerMask))
            {
                if (hit.transform == enemy.transform)
                {
                    enemy.TakeDamage(damage);

                    //DEBUG PARA VER RAYCAST
                    // Debug.DrawRay(transform.position, dir, Color.blue);
                    // Debug.Break();
                }
            }
        }

        //reseta o timer em que o jogador pode atirar novamente
        nextTimeToFire = Time.time + fireRate;
    }
}
