using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicSpawn : MonoBehaviour
{
    [SerializeField] List<Vector3> spawnPoints;
    [SerializeField] List<GameObject> enemies;

    [SerializeField] int startSpawnCount = 2;
    [Space]
    [SerializeField] float timeToIncrease;
    [SerializeField] int increaseCount;
    [SerializeField] int maxSpawnCount;
    [SerializeField] float timeMultOnIncrease;
    [SerializeField] float minTime = 0.5f;

    [Header("Forces")]
    [SerializeField] float spawnForceMin;
    [SerializeField] float spawnForceMax;
    [SerializeField] float vertSpawnForce;

    private float timer;
    private int spawnCount;

    private void Start()
    {
        spawnCount = startSpawnCount;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {

            for(int i = 0; i < spawnCount; i++)
            {
                SpawnEnemy();
            }

            timeToIncrease = Mathf.Max(timeToIncrease * timeMultOnIncrease, minTime);
            spawnCount = Math.Max(spawnCount + increaseCount, maxSpawnCount);

            // Reset clock 
            timer = timeToIncrease;
        }
    }

    private void SpawnEnemy()
    {
        Vector3 point = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Count)];
        GameObject enemy = enemies[UnityEngine.Random.Range(0, enemies.Count)];

        GameObject temp = Instantiate(enemy, point, Quaternion.identity);
        temp.transform.parent = this.transform;

        float spawnForce = UnityEngine.Random.Range(spawnForceMin, spawnForceMax);
        temp.GetComponent<Rigidbody2D>().AddForce(new Vector2(point.x < 0 ? spawnForce : -spawnForce, vertSpawnForce), ForceMode2D.Impulse);
    }

    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            Gizmos.DrawSphere(spawnPoints[i], 0.1f);
        }
    }
}
