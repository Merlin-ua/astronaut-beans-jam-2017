using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBehaviour : MonoBehaviour {
    float movementSpeed = -10.0f;

	void Start () {
		
	}

	void Update () {
        transform.Translate(new Vector3(Time.deltaTime * movementSpeed, 0.0f));
        if(transform.position.x < -50f) {
            Destroy(this.gameObject);
        }
	}
}
