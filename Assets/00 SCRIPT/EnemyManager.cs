using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float _delaySpawn;
    [SerializeField] int _maxNumEnemy;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemy()
    {
        for (int i = 0; i< _maxNumEnemy; i++)
        {
            Instantiate(enemyPrefab, this.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(_delaySpawn);
        }
    }
}
