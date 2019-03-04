using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    //credit to https://answers.unity.com/questions/989092/do-you-know-any-patrolling-ai-script-for-a-navmesh.html
    public GameObject player;
    public GameObject[] patrolPoints;
    private int patrolPoint = 0;
    public NavMeshAgent agent;
    public float stoppingDistance;

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
        agent.Resume();
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
        agent.Stop();
        Debug.Log("I see player");

    }

}
