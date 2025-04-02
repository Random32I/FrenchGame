using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] float health = 100;
    [SerializeField] GameObject[] items;
    [SerializeField] GameObject player;

    public bool inHealingZone;

    public static bool isSeatedMode;
    public bool isEnviornmentCorrectSize = false;

    [SerializeField] float SeatedScale;

    // Start is called before the first frame update
    void Start()
    {
        if (isSeatedMode)
        {
            player.transform.localScale = Vector3.one * SeatedScale;
            isEnviornmentCorrectSize = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (health == 0)
        {
            OnDeath();
        }
        if (inHealingZone)
        {
            Heal(0.05f);
        }

        if (isSeatedMode && !isEnviornmentCorrectSize)
        {
            player.transform.localScale = Vector3.one * SeatedScale;
            isEnviornmentCorrectSize = true;
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

    public void Heal(float amount)
    {
        health += amount;
    }

    public void SpawnItem(int itemID, Vector3 spawnPos)
    {
        GameObject newItem = Instantiate(items[itemID]);

        newItem.transform.position = new Vector3(spawnPos.x, 1, spawnPos.z);
    }

    void OnDeath()
    {
        Destroy(player);
        SceneManager.LoadScene("MainMenu");
    }
}
