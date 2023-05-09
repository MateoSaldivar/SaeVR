using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Gun : MonoBehaviour
{
    public Transform gunPoint;
    public AudioClip emptyGunSound;
    public AudioClip shotSound;
    public float fireSpeed = 20f;
    public float coneRadius = 1f;    // The radius of the cone
    public float coneDistance = 10f; // The maximum distance the cone can travel


    private void Start() {
		XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(OnShoot);
	}
    /*public void OnShoot(ActivateEventArgs arg) {

        if (Player.instance.bullets > 0) {
            Player.instance.bullets--;
            AudioSource.PlayClipAtPoint(shotSound, transform.position);
            RaycastHit hit;
            //ADD PARTICLES
            if (Physics.Raycast(gunPoint.position, gunPoint.forward, out hit)) {
                if (hit.collider.CompareTag("Player")) {
                    // Do something when hitting a player
                    hit.collider.GetComponent<Player>().OnGunHit();
                } else if (hit.collider.CompareTag("Enemy")) {
                    hit.collider.GetComponent<Enemy>().OnHit();
                    // Do something when hitting an enemy

                }
            }
        } else {
            AudioSource.PlayClipAtPoint(emptyGunSound, transform.position);
        }
    }*/
    public void OnShoot(ActivateEventArgs arg) {
        if (Player.instance.bullets > 0) {
            Player.instance.bullets--;
            AudioSource.PlayClipAtPoint(shotSound, transform.position);

            RaycastHit[] hits = Physics.SphereCastAll(gunPoint.position, coneRadius, gunPoint.forward, coneDistance);

            //ADD PARTICLES

            foreach (RaycastHit hit in hits) {
                if (hit.collider.CompareTag("Enemy")) {
                    hit.collider.GetComponent<Enemy>().OnHit();
                    // Do something when hitting an enemy
                    break; // break out of the loop after the first enemy is hit
                }
            }
        } else {
            AudioSource.PlayClipAtPoint(emptyGunSound, transform.position);
        }
    }
}
