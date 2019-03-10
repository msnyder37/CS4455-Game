using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioManager : MonoBehaviour
{
	public AudioClip playerDamageTaken;
	public AudioClip playerGunShot;
	public AudioClip playerFalling;
    public AudioClip doorOpening;
    public AudioClip enemyRobotDeath;
	public AudioClip enemyRobotGunShot;
	public EventSound3D eventSound3DPrefab;

	private UnityAction<PlayerHealthDamage> playerDamageEventListener;
	private UnityAction<RobotHeroController> playerFallingListener;
	private UnityAction<RobotHeroController> playerGunshotEventListener;
    private UnityAction<GameObject> doorOpeningListener;
    private UnityAction<EnemyController> robotDeathListener;
	private UnityAction<EnemyController> robotGunShotListener;

	/*template for easy copying
		public AudioClip audio
		private UnityAction<script> name;
		name =  new UnityAction<script>(handler);
		EventManager.StartListening<Event, script>(name);
		EventManager.StopListening<Event, script>(name);
		void handler(script) {
			if (audio) {
				EventSound3D snd = Instantiate(eventSound3DPrefab, script.transform.position, Quaternion.identity, null);
				snd.audioSrc.clip = this.audio;
	            snd.audioSrc.minDistance = 5f; //or whatever disatances you want here
	            snd.audioSrc.maxDistance = 100f;
	            snd.audioSrc.Play();
			}
		}
	*/

    void Awake()
    {
    	playerDamageEventListener = new UnityAction<PlayerHealthDamage>(playerDamageEventHandler);
    	playerGunshotEventListener = new UnityAction<RobotHeroController>(playerGunshotEventHandler);
    	playerFallingListener =  new UnityAction<RobotHeroController>(playerFallingEventHandler);
        doorOpeningListener = new UnityAction<GameObject>(doorOpeningHandler);
        robotDeathListener = new UnityAction<EnemyController>(robotDeathEventHandler);
    	robotGunShotListener = new UnityAction<EnemyController>(robotGunShotEventHandler);

    }

    // Update is called once per frame
    void OnEnable()
    {
    	EventManager.StartListening<PlayerDamageEvent, PlayerHealthDamage>(playerDamageEventListener);
    	EventManager.StartListening<PlayerGunshotEvent, RobotHeroController>(playerGunshotEventListener);
    	EventManager.StartListening<PlayerFallingEvent, RobotHeroController>(playerFallingListener);
        EventManager.StartListening<DoorOpeningEvent, GameObject>(doorOpeningListener);
        EventManager.StartListening<RobotDeathEvent, EnemyController>(robotDeathListener);
    	EventManager.StartListening<RobotGunShotEvent, EnemyController>(robotGunShotListener);
    }

    void OnDisable()
    {
    	EventManager.StopListening<PlayerDamageEvent, PlayerHealthDamage>(playerDamageEventListener);
    	EventManager.StopListening<PlayerGunshotEvent, RobotHeroController>(playerGunshotEventListener);
    	EventManager.StopListening<PlayerFallingEvent, RobotHeroController>(playerFallingListener);
        EventManager.StopListening<DoorOpeningEvent, GameObject>(doorOpeningListener);
    	EventManager.StopListening<RobotDeathEvent, EnemyController>(robotDeathListener);
        EventManager.StopListening<RobotGunShotEvent, EnemyController>(robotGunShotListener);
    }

    void playerDamageEventHandler(PlayerHealthDamage damage)
    {

        if (playerDamageTaken)
        {

            EventSound3D snd = Instantiate(eventSound3DPrefab, GameObject.Find("Soldier").transform.position, Quaternion.identity, null);

            snd.audioSrc.clip = this.playerDamageTaken;

            snd.audioSrc.minDistance = 5f;
            snd.audioSrc.maxDistance = 100f;

            snd.audioSrc.Play();
        }


    }

    void robotDeathEventHandler(EnemyController enemy)
    {
    	if (enemyRobotDeath) {
    		EventSound3D snd = Instantiate(eventSound3DPrefab, enemy.transform.position, Quaternion.identity, null);
    		snd.audioSrc.clip = this.enemyRobotDeath;

            snd.audioSrc.minDistance = 5f;
            snd.audioSrc.maxDistance = 100f;

            snd.audioSrc.Play();
    	}
    }

    void robotGunShotEventHandler(EnemyController enemy)
    {
        if (enemyRobotGunShot) {
            EventSound3D snd = Instantiate(eventSound3DPrefab, enemy.transform.position, Quaternion.identity, null);
            snd.audioSrc.clip = this.enemyRobotGunShot;

            snd.audioSrc.minDistance = 5f;
            snd.audioSrc.maxDistance = 100f;

            snd.audioSrc.Play();
        }
    }

    void playerGunshotEventHandler(RobotHeroController player)
    {
    	if (playerGunShot) {
    		EventSound3D snd = Instantiate(eventSound3DPrefab, player.transform.position, Quaternion.identity, null);
    		snd.audioSrc.clip = this.playerGunShot;

            snd.audioSrc.minDistance = 5f;
            snd.audioSrc.maxDistance = 100f;

            snd.audioSrc.Play();
    	}
    }

    void playerFallingEventHandler(RobotHeroController player) {
		if (playerFalling) {
			EventSound3D snd = Instantiate(eventSound3DPrefab, player.transform.position, Quaternion.identity, null);
			snd.audioSrc.clip = this.playerFalling;
            snd.audioSrc.minDistance = 5f;
            snd.audioSrc.maxDistance = 100f;
            snd.audioSrc.Play();
		}
	}

    void doorOpeningHandler(GameObject door) {
            if (doorOpening) {
                EventSound3D snd = Instantiate(eventSound3DPrefab, door.transform.position, Quaternion.identity, null);
                snd.audioSrc.clip = this.doorOpening;
                snd.audioSrc.minDistance = 5f; //or whatever disatances you want here
                snd.audioSrc.maxDistance = 100f;
                snd.audioSrc.Play();
            }
        }
}
