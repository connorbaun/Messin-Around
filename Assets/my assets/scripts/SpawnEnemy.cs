using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour {
    public GameObject enemySpawnPoint;
    public GameObject enemyPrefab;


    

    public void Spawn()
    {

        
            GameObject enemy = Instantiate(enemyPrefab, enemySpawnPoint.transform.position, enemySpawnPoint.transform.rotation);
        



    }

}
