using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour {
    float bulletSpeedX = -30.0f;
    public SpriteRenderer bulletSprite;
    public Sprite[] randomSprites;
    void Start ()
    {
        bulletSprite.sprite = randomSprites[Random.Range(0, randomSprites.Length)];

    }
	
	void Update () {
        transform.Translate(new Vector3(bulletSpeedX * Time.deltaTime, 0.0f, 0.0f));

        if (transform.position.x < -30.0f)
            Destroy(this.gameObject);
	}
}
