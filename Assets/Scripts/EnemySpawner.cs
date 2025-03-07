using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject instantiableObject;

    [SerializeField] Material Gunner;
    [SerializeField] Material Swords;

    AI[] enemies = new AI[30];

    int totalActiveEnemies;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 30; i++)
        {
            enemies[i] = Instantiate(instantiableObject).GetComponent<AI>();
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
        int type = Random.Range(0, 1); //change the 1 back to a 2 to make melee enemies spawn again
        Material material = null;

        switch (type)
        {
            case 0:
                material = Gunner;
                enemies[index].name = "Gunner";
                break;
            case 1:
                material = Swords;
                enemies[index].name = "Melee";
                break;
        }

        enemies[index].Init(type, material, transform.position + new Vector3 (Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f)), index);
    }
}
