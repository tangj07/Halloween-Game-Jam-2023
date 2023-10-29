using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawn : MonoBehaviour
{
    [SerializeField]
    GameObject enemy1;//mummy
    [SerializeField] 
    GameObject enemy2;//zombie
    [SerializeField]
    Vector3 spawnPoint1 = Vector3.zero;
    [SerializeField]
    Vector3 spawnPoint2 = Vector3.zero;
    [SerializeField]
    Vector3 spawnPoint3 = Vector3.zero;
    [SerializeField]
    Vector3 spawnPoint4 = Vector3.zero;
    [SerializeField]
    public float gameTime =0;
    public float timeChange = 2f; //2 sec for now
    public float timeBetweenSpawn =4f; //gradually gets smaller 
    public float amountTimeChange = 0.05f;
    float minTime = 0.5f;
    public int spawnCount =2, spawnCountTime =10, maxSpawn=10; //gradually gets bigger start 1 and add 1 during change
    public float jumpDistanceX =5;
    GameObject tempObj;
    float nextTime=0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TimeSpawn());
    }
    void Spawn()
    {
        float point = UnityEngine.Random.Range(0, 4);
        Vector3 tempPoint = Vector3.zero;
        switch (point)
        {
            //1 and 2 left
            case 0:
                tempPoint = new Vector3(spawnPoint1.x-jumpDistanceX,spawnPoint1.y);
                break;
            case 1:
                tempPoint = new Vector3(spawnPoint2.x - jumpDistanceX, spawnPoint2.y);
                break;
            //3 and 4 right
            case 2:
                tempPoint = new Vector3(spawnPoint3.x + jumpDistanceX, spawnPoint3.y);
                break;  
            case 3:
                tempPoint = new Vector3(spawnPoint4.x + jumpDistanceX, spawnPoint4.y);
                break;

        }
        float enemyType = UnityEngine.Random.Range(0, 2);
        
        switch (enemyType)
        {
            case 0:
                tempObj = enemy1;
                break;
            case 1:
                tempObj = enemy2;
                break;
        }
        GameObject temp = Instantiate(tempObj, tempPoint, transform.rotation);
        temp.transform.parent = this.transform;
    }
    IEnumerator TimeSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenSpawn);
            for (int i = 0; i < spawnCount; i++)
            {
                Spawn();
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        //update time and count 
        gameTime = Time.time;
        int temp = (int)gameTime;
        if (gameTime >= nextTime)
        {
            if (temp % timeChange == 0 && timeBetweenSpawn > minTime)
            {
                timeBetweenSpawn -= amountTimeChange;
            }
            if (temp % spawnCountTime == 0 && spawnCount < maxSpawn)
            {
                spawnCount++;
            }
            nextTime += 1;
        }
    }
}
