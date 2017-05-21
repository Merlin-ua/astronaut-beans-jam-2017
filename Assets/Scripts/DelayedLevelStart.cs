using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DelayedLevelStart : MonoBehaviour {
    public float levelDelay = 10.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        levelDelay -= Time.deltaTime;

        if (levelDelay <= 0f)
            SceneManager.LoadScene(1);
    }
}
