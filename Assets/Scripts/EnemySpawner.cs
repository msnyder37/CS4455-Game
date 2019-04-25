using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int maxEnemies;
    public float cooldown;
    public int health;
    public float animDuration;

    public EnemyController enemyPrefab;
    public GameObject player;
    public GameObject exposionEffect;

    private float counter;
    private List<EnemyController> spawnedEnemies;

    // Start is called before the first frame update
    void Start()
    {
        this.spawnedEnemies = new List<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (counter <= 0 && spawnedEnemies.Count < maxEnemies)
        {
            this.SpawnEnemy();
            counter = cooldown;
        }
        else
        {
            counter -= Time.deltaTime;
        }

        // remove defeated enemies from the spawned list to spawn another
        spawnedEnemies.RemoveAll(enemy => enemy == null);
    }

    void SpawnEnemy()
    {
        Vector3 spawnPoint = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
        EnemyController enemy = Instantiate(this.enemyPrefab, spawnPoint, Quaternion.identity) as EnemyController;
        enemy.player = this.player;
        enemy.isWandering = true;
        this.spawnedEnemies.Add(enemy);
    }

    void OnTriggerEnter(Collider c)
    {

        if (c.gameObject.CompareTag("Bullet"))
        {
            // TODO: Play destruction animation
            health--;
            if (health <= 0)
            {
                GameObject obj = Instantiate(exposionEffect, transform.position, transform.rotation);
                AudioSource.PlayClipAtPoint(GetComponent<AudioSource>().clip, GameObject.Find("Main Camera").transform.position);
                Destroy(transform.gameObject);
                Destroy(obj, animDuration);
            }
        }

    }
}
