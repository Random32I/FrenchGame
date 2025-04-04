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

    [SerializeField] Rigidbody[] ragdoll;
    [SerializeField] GameObject[] Weapons;

    [SerializeField] int state;

    int enemyIndex;

    float delayToAgro;
    float distanceToAgro;
    float agroTimeStamp;
    bool onAgroTimer;

    float coolDownTimeStamp;

    //Animations
    [SerializeField] Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        agent.destination = transform.position + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
    }

    public void Init(int type, Material material, Vector3 startPos, int index, EnemySpawner Spawner, float agroDelay, float agroDistance, AudioClip attackSound)
    {
        enemyType = type;
        //rig.constraints = RigidbodyConstraints.fre; freeze rotations
        enemyIndex = index;
        //GetComponent<MeshRenderer>().material = material;
        transform.position = startPos;
        transform.tag = "Sliceable";
        spawner = Spawner;
        delayToAgro = agroDelay;
        distanceToAgro = agroDistance;
        shoot.clip = attackSound;
        anim.SetBool("Gunner", type == 0);
        Weapons[type].SetActive(true);
        Wander();
    }

    // Update is called once per frame
    void Update()
    {
        if (game.paused)
        {
            if (agent.enabled) agent.enabled = false;
        }
        else
        {
            if (!agent.enabled) agent.enabled = true;
            if (game.deathTimerStarted)
            {
                state = 3;
            }
            //Debug.Log(agent.destination);
            switch (state)
            {
                case 0:
                    Wander();
                    break;
                case 1:
                    Approach();
                    if (game.inHealingZone)
                    {
                        state = 3;
                        return;
                    }
                    break;
                case 2:
                    Attack();
                    if (Mathf.Abs((transform.position - player.position).magnitude) > agent.stoppingDistance + 1)
                    {
                        state = 3;
                        return;
                    }
                    if (game.inHealingZone)
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
    }

    void Wander()
    {
        agent.stoppingDistance = 0;
        agent.speed = 2;
        agent.acceleration = 2;
        if (Mathf.Abs((transform.position - player.position).magnitude) <= distanceToAgro)
        {
            if (!onAgroTimer)
            {
                agroTimeStamp = Time.timeSinceLevelLoad;
                onAgroTimer = true;
            }
            if (Time.timeSinceLevelLoad - agroTimeStamp >= delayToAgro)
            {
                state = 1;
                onAgroTimer = false;
            }
        }
        else if (Mathf.Round(transform.position.x * 5) / 5 == Mathf.Round(agent.destination.x * 5) / 5 &&
        Mathf.Round(transform.position.z) == Mathf.Round(agent.destination.z))
        {
            agent.destination = transform.position + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
            onAgroTimer = false;
        }
    }

    void Approach()
    {
        agent.speed = 3;
        agent.acceleration = 3;
        if (enemyType == 0) agent.stoppingDistance = 5;
        if (enemyType == 1) agent.stoppingDistance = 1;


        if (Mathf.Abs((transform.position - player.position).magnitude) > distanceToAgro + 5)
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
        anim.SetBool("IsAttacking", true);
        anim.SetInteger("RandomAnim", Random.Range(0,2));

        if (Time.timeSinceLevelLoad - coolDownTimeStamp >= 3)
        {
            shoot.Play();
            if (Random.Range(0, 3) == 2)
            {
                game.DoDamage(15);
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
                if (Random.Range(0, 3) == 2)
                {
                    game.SpawnItem(2, transform.position + new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f)));
                }
                break;
        }

        game.SpawnItem(6, transform.position + new Vector3(Random.Range(-0.5f,0.5f),0, Random.Range(-0.5f, 0.5f)));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.transform.name.Contains("Bullet") && collision.transform.tag == "Shot") || collision.transform.name.Contains("Hand"))
        {
            gameObject.AddComponent<SliceDisapear>();
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
            gameObject.layer = 8;
            //rig.AddForce(collision.relativeVelocity, ForceMode.Impulse);
            Death();
        }
    }

    public void Death()
    {
        if (state != -1)
        {
            state = -1;
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
            }
            game.SpawnItem(6, transform.position + new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f)));
            Ragdoll();
            spawner.EnemyKilled(enemyIndex);
        }
    }

    public void Ragdoll()
    {
        foreach (Rigidbody joint in ragdoll)
        {
            joint.isKinematic = false;
        }
    }
}
