using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    //credit to https://answers.unity.com/questions/989092/do-you-know-any-patrolling-ai-script-for-a-navmesh.html
    //https://forum.unity.com/threads/solved-random-wander-ai-using-navmesh.327950/
    public GameObject player;
    public Transform spawn;
    public Transform gunOrigin;
    public GameObject[] patrolPoints;
    public NavMeshAgent agent;
    public float stoppingDistance;
    public float turnSpeed;
    public int health;
    public float bulletSpeed;
    public float cooldown;
    public float sightRadius;
    public float wanderTimer;
    public float wanderRadius;
    public Transform bullet;
    public bool isStationary;
    public bool isWandering;

    private int patrolPoint = 0;
    private float shotClock;
    private float changeWanderDirectionTimer;


    void Start()
    {
        stoppingDistance = .4f;
        changeWanderDirectionTimer = wanderTimer;


    }

    void Update()
    {

        if (!Physics.Linecast(transform.position, player.transform.position, 10) && Vector3.Distance(transform.position, player.transform.position) < sightRadius)
        {
            Attack();
        }
        else
        {
            if (!isStationary)
            {
                if (isWandering)
                {
                    changeWanderDirectionTimer += Time.deltaTime;
                    if (changeWanderDirectionTimer >= wanderTimer)
                    {
                        Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                        agent.SetDestination(newPos);
                        changeWanderDirectionTimer = 0;
                    }

                }
                else
                {

                    Patrol();
                }
            }
            else
            {
                agent.isStopped = true;
            }
        }

    }

    void Patrol()
    {
        agent.isStopped = false;
        if (!agent.pathPending && agent.remainingDistance < stoppingDistance)
        {
            if (patrolPoints.Length > 0)
            {
                agent.SetDestination(patrolPoints[patrolPoint].transform.position);

                patrolPoint++;

                if (patrolPoint >= patrolPoints.Length)
                {
                    patrolPoint = 0;
                }
            }
        }
    }

    void Attack()
    {
        agent.isStopped = true;
        GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
        Shoot();


    }

    void OnTriggerEnter(Collider c)
    {

        if (c.gameObject.CompareTag("Bullet"))
        {
            // TODO: Play destruction animation
            health--;
            if (health <= 0)
            {
                Destroy(transform.gameObject);
                EventManager.TriggerEvent<RobotDeathEvent, EnemyController>(this);

            }
        }
        if (c.gameObject.CompareTag("Player"))
        {
            // Rigidbody rb = c.gameObject.GetComponent<Rigidbody>();
            // rb.AddForce(rb.velocity *= -3);
            // Debug.Log("in me");
        }
    }

    void Shoot()
    {

        shotClock -= Time.deltaTime;
        if (shotClock <= 0)
        {
            // Debug.Log("Shoot");
            shotClock = cooldown;
            Transform bc = Instantiate(bullet, new Vector3(gunOrigin.position.x, gunOrigin.position.y, gunOrigin.position.z), this.transform.rotation);
            bc.gameObject.GetComponent<BulletController>().speed = bulletSpeed;
            GetComponent<AudioSource>().Play();

        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

}
