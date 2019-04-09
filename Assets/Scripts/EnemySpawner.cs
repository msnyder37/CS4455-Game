using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public int maxEnemies;
	public float cooldown;
	public int health;
	public float animDuration;
	public Transform enemyType;
	public GameObject player;
	public GameObject animation;

	private float counter;
	private List<GameObject> enemiesSpawned;
    // Start is called before the first frame update
    void Start()
    {
    	enemiesSpawned = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
    	if (counter <= 0 && enemiesSpawned.Count < maxEnemies) {
    		SpawnEnemy();
    		counter = cooldown;
    	} else {
    		counter -= Time.deltaTime;
    	}
    	foreach (GameObject g in enemiesSpawned) {
    		if (g == null) {
    			enemiesSpawned.Remove(g);
    		}
    	}
    }

    void SpawnEnemy()
    {
    	var newEnemy = Instantiate(enemyType, transform.position, Quaternion.identity);
    	newEnemy.gameObject.GetComponent<EnemyController>().player = player;
    	// newEnemy.GameObject.GetComponent<EnemyController>.chasesSpeed = .05;
    	enemiesSpawned.Add(newEnemy.gameObject);
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

            }
        }

    }
}
