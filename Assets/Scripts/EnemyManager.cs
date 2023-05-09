using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    // Queue to hold NavMeshAgents that need pathfinding
    public static EnemyManager instance;
    private static Queue<NavMeshAgent> agentQueue = new Queue<NavMeshAgent>();
    private NavMeshAgent prevAgent;
    public Transform[] spawnLocations;
    public GameObject prefabEnemy;
    public List<Enemy> EnemiesAlive = new List<Enemy>();
    public List<Enemy> EnemiesDead = new List<Enemy>();
    public float timeToSpawn = 3;
    public float difficultyIncreaser = 0.001f;
    private float CurrentTimeToSpawn = 3;
    private int enemyCount = 0;
    public int maxEnemies = 50;
    private void Awake() {
		instance = this;
	}

	// Static function to add a NavMeshAgent to the queue
	public static void RequestPath(NavMeshAgent agent) {
        if (!agentQueue.Contains(agent)) agentQueue.Enqueue(agent);
    }

    void Update() {
        // Process the queue if it's not empty
        if (agentQueue.Count > 0) {
            NavMeshAgent agent = agentQueue.Dequeue();
            if (agent != prevAgent && agent.isActiveAndEnabled) {
                agent.SetDestination(Player.instance.transform.position);
                prevAgent = agent;
            } else {
                prevAgent = null;
            }
        }

        //Spawn enemies
        CurrentTimeToSpawn -= Time.deltaTime;

        if(CurrentTimeToSpawn <= 0) {
            timeToSpawn -= difficultyIncreaser;
            difficultyIncreaser += difficultyIncreaser;
            CurrentTimeToSpawn = Mathf.Max(timeToSpawn,0.5f);
			SpawnEnemy(spawnLocations[Random.Range(0,spawnLocations.Length)].position);
		}
    }

    public void SpawnEnemy(Vector3 position) {
        if(EnemiesDead.Count > 0) {
            EnemiesDead[0].Spawn(position);
		} else {
            if (enemyCount < maxEnemies) {
                enemyCount++;
                GameObject enemy = Instantiate(prefabEnemy, position, Quaternion.identity);
                enemy.GetComponent<Enemy>().Spawn(position);
            }
		}
	}
}
