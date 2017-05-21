using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour {
    public Player playerScript;
    public float minY = -10.0f;
    public float maxY = 30.0f;
    public float moveMultiplier = 3.0f;
    
    public Vector3 offset;

    private Transform player;
    private Vector2 velocityVec;

    void Awake() {
        velocityVec = Vector2.zero;
        player = playerScript.transform;
    }

    void Start () {
		
	}

    void Update() {
        velocityVec = Vector2.MoveTowards(velocityVec, playerScript.GetVelocity(), 0.1f);

        // smooth position update
        Vector3 newPosition = new Vector3(
            player.position.x,
            Mathf.Lerp(transform.position.y, player.position.y, moveMultiplier),
            0.0f) + 
            new Vector3(
                0.0f, 
                Mathf.Clamp(velocityVec.y, -1f, 1f),
                0.0f);

        transform.position = new Vector3(newPosition.x, Mathf.Clamp(newPosition.y, minY, maxY), newPosition.z) + offset;
    }
}
