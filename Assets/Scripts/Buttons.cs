using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    [SerializeField] GameObject SettingsMenu;
    [SerializeField] GameObject NormalMenu;
    [SerializeField] GameObject player;
    [SerializeField] GameManager game;
    [SerializeField] Material DefaultTexture;
    [SerializeField] Material PaperTexture;
    [SerializeField] GameObject Paper;
    [SerializeField] GameObject Credits;

    public void Play()
    {
        Destroy(player);
        SceneManager.LoadScene("Test2");
    }

    public void Options()
    {
        SettingsMenu.SetActive(true);
        NormalMenu.SetActive(false);
        Paper.GetComponent<MeshRenderer>().material = PaperTexture;
    }

    public void Back()
    {
        SettingsMenu.SetActive(false);
        NormalMenu.SetActive(true);
        Paper.GetComponent<MeshRenderer>().material = DefaultTexture;
    }

    public void SeatedPlay()
    {
        bool state = gameObject.GetComponent<Toggle>().isOn;
        game.SetSeatedPlay(state);
    }

    public void VolumeChanged()
    {
        float amount = gameObject.GetComponent<Slider>().value;
        game.SetVolume(amount);
    }

    public void OpenCredits()
    {
        Credits.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Resume()
    {
        game.TogglePause();
    }

    public void TitleScreen()
    {
        Destroy(player);
        SceneManager.LoadScene("MainMenu");
    }
}
