using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Heart : MonoBehaviour
{
    [SerializeField] GameManager game;
    [SerializeField] AudioSource Bread;

    // Start is called before the first frame update
    void Start()
    {
        game = GameObject.Find("GameManager").GetComponent<GameManager>();
        Bread = GameObject.Find("BreadEat").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name.Contains("Hand"))
        {
            Bread.Play();
            Heal();
        }
    }

    public void Heal()
    {
        if (game.GetHealth() < 1000)
        {
            game.Heal(5);

            Destroy(gameObject);
        }
    }
}
