using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segment : MonoBehaviour {
	public Passage parentPassage;
	public Passage.Type type;
	void Start() {
		parentPassage = GetComponentInParent<Passage>();
		type = parentPassage.type;
	}

	void Update() {

	}

	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Platform")) {
			if (type != Platform.instance.currentPassageType) {
				MapManager.instance.AddNewPassage();
				Platform.instance.currentPassageType = type;
			}
		}
	}
}
