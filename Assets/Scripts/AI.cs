using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] GameManager game;

    int state;

    // Start is called before the first frame update
    void Start()
    {
        agent.destination = transform.position + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(agent.destination);
        switch (state) 
        {
            case 0:
                Wander();
                break;
            case 1:
                Approach();
                break;
            case 2:
                Attack();
                break;
            case 3:
                Evacuate();
                break;
        }
    }

    void Wander()
    {
        agent.stoppingDistance = 0;
        agent.speed = 2;
        agent.acceleration = 2;
        agent.angularSpeed = 30;
        if (Mathf.Round(transform.position.x * 10)/10 == Mathf.Round(agent.destination.x * 10) / 10 &&
            Mathf.Round(transform.position.z * 10) / 10 == Mathf.Round(agent.destination.z * 10) / 10)
        {
            agent.destination = transform.position + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
        }
    }

    void Approach()
    {
        agent.speed = 8;
        agent.angularSpeed = 120;
        agent.acceleration = 8;
        agent.stoppingDistance = 5;
        agent.destination = new Vector3(player.position.x * -1, transform.position.y, player.position.z * -1);
    }

    void Attack()
    {
        game.DoDamage(1);
    }

    void Evacuate()
    {
        //Transform.position.y instead of player.position.y so that the ai isnt trying to path in the y direction
        agent.destination = new Vector3(player.position.x * -1, transform.position.y, player.position.z * -1);
    }
}
