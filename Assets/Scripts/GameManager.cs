using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] int health = 100;
    [SerializeField] GameObject[] items;
    [SerializeField] GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health == 0)
        {
            OnDeath();
        }
    }

    public float GetHealth()
    {
        return health;
    }

    public void DoDamage(int damage)
    {
        health -= damage;
    }

    public void Heal()
    {
        health++;
    }

    public void SpawnItem(int itemID, Vector3 spawnPos)
    {
        GameObject newItem = Instantiate(items[itemID]);

        newItem.transform.position = spawnPos;
    }

    void OnDeath()
    {
        Destroy(player);
        SceneManager.LoadScene("MainMenu");
    }
}
