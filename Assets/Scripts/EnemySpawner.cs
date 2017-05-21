using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public GameObject enemyPrefab;
	// Use this for initialization
	void Start () {
		if(Random.Range(0,10) > 5) {
            GameObject newEnemy = GameObject.Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            newEnemy.transform.SetParent(this.transform.parent);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
