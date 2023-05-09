using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    public static AmmoManager instance;
    // Start is called before the first frame update
    public List<Ammo> ActiveAmmo = new List<Ammo>();
    public List<Ammo> InactiveAmmo = new List<Ammo>();
    public GameObject prefabAmmo;
    private int AmmoCount = 0;
    public int maxAmmo = 10;
	private void Awake() {
		instance = this;
	}
	public void SpawnAmmo(Vector3 position) {
        if (InactiveAmmo.Count > 0) {
            InactiveAmmo[0].Spawn(position);
        } else {
            if (AmmoCount < maxAmmo) {
                AmmoCount++;
                GameObject enemy = Instantiate(prefabAmmo, position, Quaternion.identity);
                enemy.GetComponent<Enemy>().Spawn(position);
            }
        }
    }
}
