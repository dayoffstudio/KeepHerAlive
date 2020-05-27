using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public List<GameObject> monster;
    public int count;
    void Start()
    {
        SpawnerEnemy();
    }

    void Update()
    {
        
    }

    public void SpawnerEnemy()
    {
        Vector3 spawnerPosition = new Vector3();
        for (int i = 0; i < count*2/25; i++)
        {
            spawnerPosition.x = Random.Range(-75.0f, -60.0f);
            spawnerPosition.y = Random.Range(1.0f, 26.0f);
            Instantiate(monster[Random.Range(0, monster.Count)], spawnerPosition, Quaternion.identity);
        }
        for(int i= count * 2 / 25; i< count * 27 / 50;i++)
        {
            spawnerPosition.x = Random.Range(-75.0f, 30.0f);
            spawnerPosition.y = Random.Range(-15.0f, 2.5f);
            Instantiate(monster[Random.Range(0, monster.Count)], spawnerPosition, Quaternion.identity);
        }
        for (int i = count * 27 / 50; i < count ; i++)
        {
            spawnerPosition.x = Random.Range(-75.0f, -38.0f);
            spawnerPosition.y = Random.Range(-71.0f, -20.5f);
            Instantiate(monster[Random.Range(0, monster.Count)], spawnerPosition, Quaternion.identity);
        }
        
    }
}
