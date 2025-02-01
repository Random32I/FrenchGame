using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{

    [SerializeField] Transform player;
    [SerializeField] NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Approach();
    }

    void Approach()
    {
        agent.destination = player.position;
    }
}
