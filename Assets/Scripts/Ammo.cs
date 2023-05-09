using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public bool Alive = false;


    public void Spawn(Vector3 position) {
        if (AmmoManager.instance.InactiveAmmo.Contains(this)) AmmoManager.instance.InactiveAmmo.Remove(this);
        if (!AmmoManager.instance.ActiveAmmo.Contains(this)) AmmoManager.instance.ActiveAmmo.Add(this);
        transform.position = position;
        Alive = true;
    }

    public void Despawn() {
        if (AmmoManager.instance.ActiveAmmo.Contains(this)) AmmoManager.instance.ActiveAmmo.Remove(this);
        if (!AmmoManager.instance.InactiveAmmo.Contains(this)) AmmoManager.instance.InactiveAmmo.Add(this);
        Alive = false;
        gameObject.SetActive(false);
    }


	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player")) {
            Player.instance.bullets += 10;
            Despawn();
		}
	}
}
