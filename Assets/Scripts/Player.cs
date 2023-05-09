using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public int score=0;
    public short hp=5;
    public short bullets=5;

    // Start is called before the first frame update
    void Awake() {
        instance = this;
    }

    // Update is called once per frame
    void Update() {
        SetUiValues();
    }

    void SetUiValues() {
        UIRef ui = UIRef.instance;

        ui.ScoreText.text = "Score: " + score;
        ui.HPText.text = "HP: "+hp;
        ui.AmmoText.text = "Bullets: " + bullets;
	}

    public void OnGunHit() {
        //KILL PLAYER< CALL GAMEMANAGER
	}
}
