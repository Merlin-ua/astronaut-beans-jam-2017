using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {
    public Transform muzzle;
    public GameObject bulletPrefab;
    Transform player;
    bool isFiring = true;

    float fireRate = 2.0f;
    float fireTimer = 0.0f;
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
	
	// Update is called once per frame
	void Update () {
		if(transform.position.x < 0.0f) {
            isFiring = false;
        }

        if(isFiring && fireTimer >= fireRate) {
            fireTimer = 0f;
            GameObject.Instantiate(bulletPrefab, muzzle.position, Quaternion.identity);
        }
        
        fireTimer += Time.deltaTime;

    }
}
