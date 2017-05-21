using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxSprite : MonoBehaviour {
    public float speedY = -10.0f;
    public Transform relatedTransform;
    RectTransform rectTransform;
    float initialTop = 0f;
    float initialBot = 0f;
    Vector3 initialVec;
    Vector3 initialRelatePos;
    // Use this for initialization
    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
        initialRelatePos = relatedTransform.position;
        initialVec = transform.localPosition;
    }

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if(relatedTransform.position.y > initialRelatePos.y) {
            float newY = initialVec.y + ((relatedTransform.position.y - initialRelatePos.y) * speedY);
           // newY = Mathf.Clamp(newY, -10000.0f, initialVec.y);
            transform.localPosition =
                new Vector3(
                    initialVec.x,
                    newY,
                    initialVec.z);
        }
    }
}
