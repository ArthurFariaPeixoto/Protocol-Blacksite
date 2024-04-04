using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    //Alcance horizontal da arma
    public float range = 20f;

    //Alcance vertical da arma
    public float verticalRange = 20f;

    public float gunShotRadius = 20f;

    //dano do tiro
    public float damage = 1f;

    // fire rate kkkkkkkk
    public float fireRate = 0.3f;

    // variavel para evitar spam de tiros
    private float nextTimeToFire;

    public int ammoAmount;
    public int MaxAmmoAmount;

    //Box Collider da arma para verificar se existe algum inimigo no range
    private BoxCollider gunTrigger;

    public EnemyManager enemyManager;

    public LayerMask raycastLayerMask;
    public LayerMask enemyLayerMask;

    void Start()
    {
        //Inicia o box collider e o posiciona
        gunTrigger = GetComponent<BoxCollider>();
        gunTrigger.size = new Vector3(1, verticalRange, range);
        gunTrigger.center = new Vector3(0, 0, range * 0.5f);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time > nextTimeToFire && ammoAmount > 0)
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
        // Ativa a agressividade dos inimigos próximos
        Collider[] enemyColliders = Physics.OverlapSphere(transform.position, gunShotRadius, enemyLayerMask);
        foreach (var enemyCollider in enemyColliders)
        {
            EnemyAwareness enemyAwareness = enemyCollider.GetComponent<EnemyAwareness>();
            if (enemyAwareness != null)
            {
                enemyAwareness.isAggro = true;
            }
        }
        // Reproduz o som de tiro
        if (TryGetComponent(out AudioSource audioSource))
        {
            audioSource.Stop();
            audioSource.Play();
        }
        // Aplica dano a todos os inimigos em alcance
        foreach (var enemy in enemyManager.enemiesInRange)
        {
            Vector3 dir = enemy.transform.position - transform.position;
            if (Physics.Raycast(transform.position, dir, out RaycastHit hit, range * 1.5f, raycastLayerMask) && hit.transform == enemy.transform)
            {
                enemy.TakeDamage(damage);
                // DEBUG: Desenhar raio para depuração
                // Debug.DrawRay(transform.position, dir, Color.blue);
                // Debug.Break();
            }
        }
        // Reseta o temporizador para o próximo disparo
        nextTimeToFire = Time.time + fireRate;

        ammoAmount--;
    }

    public void GiveAmmo(int amount, GameObject pickup)
    {
        if (ammoAmount < MaxAmmoAmount)
        {
            ammoAmount += amount;
            Destroy(pickup);
        }
        if (ammoAmount > MaxAmmoAmount)
        {
            ammoAmount = MaxAmmoAmount;
        }
    }


}