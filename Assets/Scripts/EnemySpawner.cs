using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] EnemySpawner[] OtherSpawners = new EnemySpawner[3];
    [SerializeField] AudioClip[] AttackSounds;

    [SerializeField] Material Gunner;
    [SerializeField] Material Swords;

    int totalActiveEnemies;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            totalActiveEnemies++;
            SpawnEnemy(i);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnemyKilled(int index)
    {
        SpawnEnemy(index);
    }

    public void SpawnEnemy(int index)
    {
        int type = Random.Range(0, 2); //change the 1 back to a 2 to make melee enemies spawn again
        int spawner = Random.Range(0, 3);
        float agroDelay = Random.Range(0.5f, 3f);
        float agroDistance = Random.Range(7f, 13f);
        Material material = null;

        GameObject newEnemy = Instantiate(enemyPrefab);

        switch (type)
        {
            case 0:
                material = Gunner;
                newEnemy.name = "Gunner";
                break;
            case 1:
                material = Swords;
                newEnemy.name = "Melee";
                break;
        }
        newEnemy.SetActive(true);
        newEnemy.GetComponent<AI>().Init(type, material, transform.position + new Vector3 (Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f)), index, OtherSpawners[spawner], agroDelay, agroDistance, AttackSounds[type]);
    }
}
