using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject enemyPrefab;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }
    public IEnumerator speedBoost()
    {
            int randSpawnPoint = Random.Range(0,spawnPoints.Length);
            
            yield return new WaitForSeconds(5f);

            Instantiate(gameObject, spawnPoints[randSpawnPoint].position, transform.rotation);
    }
    
}
