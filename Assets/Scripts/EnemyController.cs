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
    public int health;
    public GameObject animation;
    public float animDuration;
    public float stoppingDistance;
    public float turnSpeed;
    public float bulletSpeed;
    public float cooldown;
    public float sightRadius;
    public float wanderTimer;
    public float wanderRadius;
    public float chaseSpeed = .1f;
    public float normalSpeed = .05f;
    public Transform bullet;
    public bool isStationary;
    public bool isWandering;
    public bool chasesPlayer;
    public bool isTurret= false;

    private int patrolPoint = 0;
    private float shotClock;
    private float changeWanderDirectionTimer;
    private Animator animator;

    void Start()
    {
        stoppingDistance = .4f;
        changeWanderDirectionTimer = wanderTimer;
        animator = GetComponent<Animator>();
        agent.updatePosition = false;
    }

    void Update()
    {
        Vector3 globalVelocity = agent.velocity;

        // convert to local frame
        float forward = Vector3.Dot(globalVelocity, this.transform.forward);
        float right = Vector3.Dot(globalVelocity, this.transform.right);
        Vector3 localVelocity = new Vector3(right, 0, forward);
        localVelocity = Vector3.Normalize(localVelocity);

        this.animator.SetFloat("Right", localVelocity.x);
        this.animator.SetFloat("Forward", localVelocity.z);

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
        agent.speed = normalSpeed;
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
        if(isTurret) {
            transform.Rotate(0, -90, 0, Space.World);
        }
        Shoot();
        if (chasesPlayer) {
            agent.isStopped = false;
            if(!agent.pathPending && Vector3.Distance(transform.position, player.transform.position) < stoppingDistance / 2) {
                agent.SetDestination(player.transform.position);
                agent.speed = chaseSpeed;
            }

        }

    }

    void OnTriggerEnter(Collider c)
    {

        if (c.gameObject.CompareTag("Bullet"))
        {
            // TODO: Play destruction animation
            health--;
            if (health <= 0)
            {
                GameObject obj = Instantiate(animation, transform.position, transform.rotation);
                Destroy(transform.gameObject);
                Destroy(obj, animDuration);
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

    void OnAnimatorMove()
    {
        Vector3 position = animator.rootPosition;
        transform.position = position;
        agent.nextPosition = position;
    }

    void Shoot()
    {

        shotClock -= Time.deltaTime;
        if (shotClock <= 0)
        {
            // Debug.Log("Shoot");
            shotClock = cooldown;
            Transform bc;
            if (isTurret) {
                bc = Instantiate(bullet, new Vector3(gunOrigin.position.x, gunOrigin.position.y, gunOrigin.position.z), Quaternion.Euler(new Vector3(transform.eulerAngles.x,
                    transform.eulerAngles.y + 90,
                    transform.eulerAngles.z)));

                } else {
                    bc = Instantiate(bullet, new Vector3(gunOrigin.position.x, gunOrigin.position.y, gunOrigin.position.z), transform.rotation);
                }


            bc.gameObject.GetComponent<BulletController>().speed = bulletSpeed;
            EventManager.TriggerEvent<RobotGunShotEvent, EnemyController>(this);

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
