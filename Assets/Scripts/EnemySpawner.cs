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

    public void SpawnEnemy(int index)
    {
        int type = Random.Range(0, 2);
        Material material = null;

        switch (type)
        {
            case 0:
                material = Gunner;
                break;
            case 1:
                material = Swords;
                break;
        }

        enemies[index].Init(type, material, transform.position + new Vector3 (Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f)));
    }
}
