using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Valve.VR;

public class GameManager : MonoBehaviour
{
    [SerializeField] float health = 100;
    [SerializeField] GameObject[] items;
    [SerializeField] GameObject player;
    [SerializeField] Transform playerPos;
    [SerializeField] GameObject Tent;
    [SerializeField] TextMeshProUGUI healthText;

    public bool inHealingZone;

    public bool paused;

    [SerializeField] AudioSource[] audioSources;

    public static float Volume;
    bool isVolumeSet = false;

    public static bool isSeatedMode;
    public bool isEnviornmentCorrectSize = false;

    public bool deathTimerStarted = false;
    float deathTimeStamp;
    [SerializeField] GameObject DeathMenu;

    [SerializeField] GameObject Pointer;
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject teleporting;
    [SerializeField] SteamVR_Action_Boolean pause;


    // Start is called before the first frame update
    void Start()
    {
        if (isSeatedMode)
        {
            player.transform.localScale = Vector3.one * 1.3f;
            isEnviornmentCorrectSize = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu)
        {
            if (pause.stateDown)
            {
                TogglePause();
            }
        }

        if (Tent)
        {
            if ((Tent.transform.position - playerPos.position).magnitude <= 15)
            {
                inHealingZone = true;
            }
            else
            {
                inHealingZone = false;
            }
            if (healthText)
            {
                healthText.text = $"{(int)health}";
            }
        }
        if (health <= 0)
        {
            health = 0;
            OnDeath();
        }
        if (inHealingZone)
        {
            Heal(0.05f);
        }

        if (isSeatedMode && !isEnviornmentCorrectSize)
        {
            player.transform.localScale = Vector3.one * 1.3f;
            isEnviornmentCorrectSize = true;
        }
        else if (!isSeatedMode && !isEnviornmentCorrectSize)
        {
            player.transform.localScale = Vector3.one;
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

    public void TogglePause()
    {
        teleporting.SetActive(!teleporting.activeSelf);
        PauseMenu.SetActive(!PauseMenu.activeSelf);
        Pointer.SetActive(!Pointer.activeSelf);
        paused = !paused;
    }

    public void SetSeatedPlay(bool state)
    {
        isSeatedMode = state;
        isEnviornmentCorrectSize = false;
    }

    public void SetVolume(float amount)
    {
        Volume = amount;
        isVolumeSet = false;
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
        if (health + amount <= 200)
        {
            health += amount;
        }
        else
        {
            health = 200;
        }
    }

    public void SpawnItem(int itemID, Vector3 spawnPos)
    {
        GameObject newItem = Instantiate(items[itemID]);

        newItem.transform.position = new Vector3(spawnPos.x, 1, spawnPos.z);
    }

    void OnDeath()
    {
        deathTimerStarted = true;
        DeathMenu.SetActive(true);
        deathTimeStamp = Time.timeSinceLevelLoad;
    }
}
