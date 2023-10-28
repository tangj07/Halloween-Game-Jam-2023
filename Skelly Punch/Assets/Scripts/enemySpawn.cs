using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawn : MonoBehaviour
{
    [SerializeField]
    GameObject enemy1;//skellotin
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

    public float gameTime =0;
    public float timeChange = 2f; //2 sec for now
    public float timeBetweenSpawn =4f; //gradually gets smaller 
    public float amountTimeChange = 0.05f;
    float minTime = 0.5f;
    public int spawnCount =2, spawnCountTime =10; //gradually gets bigger start 1 and add 1 during change
    GameObject tempObj = new GameObject();
    int interval=1;
    float nextTime=0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TimeSpawn());
    }
    void Spawn()
    {
        float point = UnityEngine.Random.Range(0, 3);
        Vector3 tempPoint = Vector3.zero;
        switch (point)
        {
            case 0:
                tempPoint = spawnPoint1;
                break;
            case 1:
                tempPoint = spawnPoint2;
                break;
            case 2:
                    tempPoint = spawnPoint3;
                break;  
            case 3:
                    tempPoint = spawnPoint4;
                break;

        }
        float enemyType = UnityEngine.Random.Range(0, 1);
        
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
    //method for when the enemy spawns and jumps onto the game screen
    void jump(){
        
        
    }
    // Update is called once per frame
    void Update()
    {
       //update time and count 
       gameTime = Time.time;
        int temp = (int)gameTime;
        if(Time.time >= nextTime)
        {
            if (temp % timeChange == 0 && timeBetweenSpawn > minTime)
            {
                timeBetweenSpawn -= amountTimeChange;
            }
            if (temp % spawnCountTime == 0)
            {
                spawnCount++;
            }
            nextTime += interval;
        }
       
    }
}
