using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    public Scrollbar jetpackValueScrollbar;
    public Player playerScript;
    public Image[] hearts;
    public Text gameoverSign;
    public Text scoreSign;
    public Text gameScore;

    void Awake() {
        gameoverSign.enabled = false;
        scoreSign.enabled = false;
    }

	void Start () {
		
	}
	
	void Update () {
        gameScore.text = playerScript.GetScore().ToString();
        // update jeptack bar
        jetpackValueScrollbar.size = playerScript.GetFuelRatio();

        // update lives
        for(int i = 0; i < hearts.Length; i++) {
            hearts[i].enabled = i + 1 <= playerScript.GetLives();
        }

        // gameover check
        if(playerScript.GetLives() <= 0) {
            scoreSign.text = "You ran " + playerScript.GetScore().ToString() + " meters";
            if (!gameoverSign.enabled) {
                gameoverSign.enabled = true;
            }
            if (!scoreSign.enabled) {
                scoreSign.enabled = true;
            }
        }
    }
}
