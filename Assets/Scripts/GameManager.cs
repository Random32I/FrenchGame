using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] float health = 100;
    [SerializeField] GameObject[] items;
    [SerializeField] GameObject player;
    [SerializeField] TextMeshProUGUI healthText;

    public bool inHealingZone;

    [SerializeField] AudioSource[] audioSources;

    public static float Volume;
    bool isVolumeSet = false;

    public static bool isSeatedMode;
    public bool isEnviornmentCorrectSize = false;

    [SerializeField] float SeatedScale;

    public bool deathTimerStarted = false;
    float deathTimeStamp;

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
        if (healthText)
        {
            healthText.text = $"{health}";
        }
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

        if (!isVolumeSet)
        {
            foreach(AudioSource source in audioSources)
            {
                source.volume = Volume;
            }
            isVolumeSet = true;
        }

        if (deathTimerStarted && Time.timeSinceLevelLoad - deathTimeStamp >= 5)
        {
            Destroy(player);
            SceneManager.LoadScene("Test2");
        }
    }

    public void SetSeatedPlay(bool state)
    {
        isSeatedMode = state;
    }

    public void SetVolume(float amount)
    {
        Volume = amount;
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
        deathTimerStarted = true;
        deathTimeStamp = Time.timeSinceLevelLoad;
    }
}
