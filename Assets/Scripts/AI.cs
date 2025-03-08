using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] GameManager game;
    [SerializeField] Rigidbody rig;
    [SerializeField] EnemySpawner spawner;
    [SerializeField] AudioSource shoot;
    [SerializeField] AudioSource PlayerHit;
    int enemyType;

    int state;

    int enemyIndex;

    float coolDownTimeStamp;

    // Start is called before the first frame update
    void Start()
    {
        agent.destination = transform.position + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
    }

    public void Init(int type, Material material, Vector3 startPos, int index, EnemySpawner Spawner)
    {
        enemyType = type;
        //rig.constraints = RigidbodyConstraints.fre; freeze rotations
        enemyIndex = index;
        GetComponent<MeshRenderer>().material = material;
        transform.position = startPos;
        Debug.Log(transform.position);
        Debug.Log(startPos);
        spawner = Spawner;
        Wander();
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
                if (enemyType == 0)
                {
                    Attack();
                }
                else if (Mathf.Abs((transform.position - player.position).magnitude) > agent.stoppingDistance + 1)
                {
                    state = 3;
                    return;
                }
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
        if (Mathf.Abs((transform.position - player.position).magnitude) <= 25)
        {
            state = 1;
        }
        else if (Mathf.Round(transform.position.x * 5) / 5 == Mathf.Round(agent.destination.x * 5) / 5 &&
        Mathf.Round(transform.position.z * 5) / 5 == Mathf.Round(agent.destination.z * 5) / 5)
        {
            agent.destination = transform.position + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
        }
    }

    void Approach()
    {
        agent.speed = 4;
        agent.angularSpeed = 120;
        agent.acceleration = 8;
        if (enemyType == 0) agent.stoppingDistance = 5;
        if (enemyType == 1) agent.stoppingDistance = 1;


        if (Mathf.Abs((transform.position - player.position).magnitude) > 25)
        {
            state = 0;
            return;
        }
        else if (Mathf.Abs((transform.position - player.position).magnitude) <= agent.stoppingDistance)
        {
            state = 2;
            return;
        }
        agent.destination = new Vector3(player.position.x * -1, transform.position.y, player.position.z * -1);
    }

    void Attack()
    {
        if (Time.timeSinceLevelLoad - coolDownTimeStamp >= 1)
        {
            shoot.Play();
            if (Random.Range(0, 4) == 3)
            {
                //game.DoDamage(1);
                PlayerHit.Play();
            }
            coolDownTimeStamp = Time.timeSinceLevelLoad;
        }
    }

    void Evacuate()
    {
        //Transform.position.y instead of player.position.y so that the ai isnt trying to path in the y direction
        agent.destination = new Vector3(player.position.x * -1, transform.position.y, player.position.z * -1);

        if (Mathf.Abs((agent.transform.position - player.transform.position).magnitude) >= 35)
        {
            state = 0;
        }
    }

    void OnDeath(Vector3 impactForce)
    {
        rig.constraints = RigidbodyConstraints.None;
        rig.AddForce(impactForce, ForceMode.Impulse);
        spawner.EnemyKilled(enemyIndex);

        switch (enemyType)
        {
            case 0:

                int bullets = Random.Range(2, 5);
                for (int i = 0; i < bullets; i++)
                {
                    game.SpawnItem(0, transform.position + new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f)));
                }
                if (Random.Range(0, 4) == 3)
                {
                    game.SpawnItem(1, transform.position + new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f)));
                }
                break;
            case 1:
                if (Random.Range(0, 4) == 3)
                {
                    game.SpawnItem(2, transform.position + new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f)));
                }
                break;
        }

        game.SpawnItem(6, transform.position + new Vector3(Random.Range(-0.5f,0.5f),0, Random.Range(-0.5f, 0.5f)));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name == "Bullet")
        {
            OnDeath(collision.transform.GetComponent<Rigidbody>().velocity);
            state = 4;
        }
    }

    public void Death()
    {
        switch (enemyType)
        {
            case 0:

                int bullets = Random.Range(2, 5);
                for (int i = 0; i < bullets; i++)
                {
                    game.SpawnItem(0, transform.position + new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f)));
                }
                if (Random.Range(0, 4) == 3)
                {
                    game.SpawnItem(1, transform.position + new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f)));
                }
                break;
            case 1:
                if (Random.Range(0, 4) == 3)
                {
                    game.SpawnItem(2, transform.position + new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f)));
                }
                break;
        }
        game.SpawnItem(6, transform.position + new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f)));
        spawner.EnemyKilled(enemyIndex);
    }
}
