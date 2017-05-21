using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {
    public GameObject[] tilePrefabs;
    public float tileSize = 30.0f;
    public Player playerScript;
    private Transform player;

    void Awake() {
        player = playerScript.transform;
    }

	void Start () {
		
	}
	void Update() {
        
    }

	void FixedUpdate () {
        // new tile logic
        float closestTileDistanceX = -1000.0f;
        GameObject[] activeTiles = GameObject.FindGameObjectsWithTag("Map");
        foreach (GameObject tile in activeTiles) {
            float tileDistanceX = transform.position.x - tile.transform.position.x;
            if (closestTileDistanceX == -1000.0f || tileDistanceX < closestTileDistanceX) {
                closestTileDistanceX = tileDistanceX;
            }
        }
        if(closestTileDistanceX >= tileSize) {
            GameObject.Instantiate(
                tilePrefabs[Random.Range(0, tilePrefabs.Length)],
                new Vector3(transform.position.x - closestTileDistanceX + tileSize, transform.position.y, transform.position.z),
                Quaternion.identity);
        }

        // gameover check
        if(player.position.y < -4.0f) {
            playerScript.Kill();
        }

        if(playerScript.GetLives() == 0) {
            Time.timeScale = 0f;
        }
    }
}
