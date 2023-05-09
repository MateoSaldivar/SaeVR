using UnityEngine;

public class Enemy : MonoBehaviour {
    public float health = 100f;
    public bool Alive = false;
    public float timeToSeekPath = 5;

    public bool debugKill = false;
    public void OnHit() {
        health -= 20f;

        if (health <= 0f) {
            Die();
        }
    }
	private void Update() {
        if (timeToSeekPath > 0 && Alive) {
            timeToSeekPath-=Time.deltaTime;
        }


		if (debugKill) {
            debugKill = false;
            Die();
		}
	}

	public void Spawn(Vector3 position) {
        if (EnemyManager.instance.EnemiesDead.Contains(this)) EnemyManager.instance.EnemiesDead.Remove(this);
        if (!EnemyManager.instance.EnemiesAlive.Contains(this)) EnemyManager.instance.EnemiesAlive.Add(this);
        transform.position = position;
        Alive = true;
        health = 100f;
	}

    private void Die() {
        if (EnemyManager.instance.EnemiesAlive.Contains(this)) EnemyManager.instance.EnemiesAlive.Remove(this);
        if (!EnemyManager.instance.EnemiesDead.Contains(this)) EnemyManager.instance.EnemiesDead.Add(this);
        if(Random.Range(0,100f) < 5f) {
            AmmoManager.instance.SpawnAmmo(transform.position);
		}
        Alive = false;
        gameObject.SetActive(false);
    }
}