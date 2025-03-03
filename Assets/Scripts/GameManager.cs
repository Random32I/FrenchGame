using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] GameObject[] items;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetHealth()
    {
        return health;
    }

    public void DoDamage(float damage)
    {
        health -= damage;
    }

    public void SpawnItem(int itemID, Vector3 spawnPos)
    {
        GameObject newItem = Instantiate(items[itemID]);

        newItem.transform.position = spawnPos;
    }
}
