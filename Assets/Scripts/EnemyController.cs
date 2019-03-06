using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    //credit to https://answers.unity.com/questions/989092/do-you-know-any-patrolling-ai-script-for-a-navmesh.html
    public GameObject player;
    public Transform spawn;
    public GameObject[] patrolPoints;
    private int patrolPoint = 0;
    public NavMeshAgent agent;
    public float stoppingDistance;
    public float turnSpeed;
    public EnemyGunController gun;
    public int health;
    public float bulletSpeed;
    public float cooldown;
    public Transform bullet;

    private float shotClock;


    void Start()
    {
        stoppingDistance = .4f;


    }

    void Update()
    {
        if(!Physics.Linecast(transform.position,player.transform.position,1)){ //check if we see player by linecasting,move player to another layer so the ray won't hit it.
             Attack();
        }else {
             Patrol();
        }

    }

    void Patrol(){
        agent.isStopped = false;
        if (!agent.pathPending && agent.remainingDistance < stoppingDistance) {
            if(patrolPoints.Length > 0){
                agent.SetDestination(patrolPoints[patrolPoint].transform.position);

                patrolPoint++;    //use distance if needed(lower precision)

                if(patrolPoint >= patrolPoints.Length){
                    patrolPoint = 0;
                }
            }
        }
    }

    void Attack(){
        agent.isStopped = true;
        GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
        Shoot();
        // Debug.Log("I see player");

    }

    void OnTriggerEnter(Collider c) {
    	//Debug.Log(c.gameObject);
    	if (c.gameObject.CompareTag("Bullet")) {
    		// TODO: Play destruction animation
    		health--;
    		if (health <= 0) {
    			Destroy(transform.gameObject);

    		}
    	}
    	if (c.gameObject.CompareTag("Player")) {
    		Rigidbody rb = c.gameObject.GetComponent<Rigidbody>();
    		rb.AddForce(rb.velocity *= -3);
    		Debug.Log("in me");
    	}
    }

    void Shoot() {
    	// if (bulletList.Count < 10) {
    	shotClock -= Time.deltaTime;
    	if (shotClock <= 0) {
                    shotClock = cooldown;
	    	Transform gun = transform.GetChild(0);
	    	Transform bc = Instantiate(bullet, new Vector3(gun.position.x, spawn.position.y, gun.position.z), gun.rotation);
	    	bc.gameObject.GetComponent<BulletController>().speed = bulletSpeed;

    	} else {
    		shotClock = 0;
    	}
    }

}
