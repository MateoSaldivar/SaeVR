using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPathFinding : MonoBehaviour {
    // NavMeshAgent component
    private NavMeshAgent agent;
    public Enemy enemySystem;
    public float VisionRange = 5.0f;

    // Timer to control how often the distance to the player is checked
    private float timer = 0.0f;

    // Time interval between distance checks
    private float timeInterval = 0.5f;

    void Start() {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update() {
        timer += Time.deltaTime;
        bool canSeePlayer = false;
        
        if (Vector3.Distance(agent.transform.position, Player.instance.transform.position) < VisionRange) {
            // Player is close, so check if the enemy can see the player
            RaycastHit hit;
            bool rayhit = Physics.Raycast(transform.position, Player.instance.transform.position - transform.position, out hit, VisionRange);
            canSeePlayer = (rayhit && hit.transform.CompareTag("Player"));
        }

        // Check if the agent doesn't have a path, or if it's close to its destination
        if (!agent.hasPath || agent.remainingDistance < 0.5f || (canSeePlayer && timer >= timeInterval) || enemySystem.timeToSeekPath <= 0) {
            enemySystem.timeToSeekPath = 5;
            EnemyManager.RequestPath(agent);
            timer = 0;
        }
    }
}
