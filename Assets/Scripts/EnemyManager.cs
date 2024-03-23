using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //Lista de inimigos no range da arma
    public List<Enemy> enemiesInRange = new List<Enemy>();

    //Adiciona inimigos na lista
    public void addEnemy(Enemy enemy)
    {
        enemiesInRange.Add(enemy);
    }

    //remove inimigos na lista
    public void removeEnemy(Enemy enemy)
    {
        enemiesInRange.Remove(enemy);
    }
}
