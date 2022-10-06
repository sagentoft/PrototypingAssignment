using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint1, spawnPoint2, spawnPoint3, spawnPoint4, spawnPoint5, spawnPoint6;
    [SerializeField] private GameObject Enemy;
    [SerializeField] private float timeBetweenSpawns;
    [SerializeField] private int enemyCount;
    [SerializeField] private int allowedEnemyCount;
    [SerializeField] private bool canSpawnEnemies = true;

    private void Update()
    {
        if (enemyCount < allowedEnemyCount && canSpawnEnemies)
        {
            SpawningStuff(1);
            enemyCount += 6;

        }
    }

    private void SpawningStuff(int enemyCount)
    {
        for (int i = enemyCount; i < allowedEnemyCount; i++)
        {
            if (canSpawnEnemies)
            {
                Instantiate(Enemy, spawnPoint1.position, transform.rotation);
                Instantiate(Enemy, spawnPoint2.position, transform.rotation);
                Instantiate(Enemy, spawnPoint3.position, transform.rotation);
                Instantiate(Enemy, spawnPoint4.position, transform.rotation);
                Instantiate(Enemy, spawnPoint5.position, transform.rotation);
                Instantiate(Enemy, spawnPoint6.position, transform.rotation);
                canSpawnEnemies = false;
            }            
            StartCoroutine(Cooldown());
        }
    }

    private void NotSpawningStuff()
    {


    }

    private void RandomSpawnPoint()
    {


    }

     IEnumerator Cooldown()
     {
         yield return new WaitForSeconds(timeBetweenSpawns);
         {
             canSpawnEnemies = true;
         }
 
    }
    

}
