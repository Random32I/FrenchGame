using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Heart : MonoBehaviour
{
    [SerializeField] GameManager game;

    // Start is called before the first frame update
    void Start()
    {
        game = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Heal()
    {
        if (game.GetHealth() < 1000)
        {
            game.Heal();

            Destroy(gameObject);
        }
    }
}
