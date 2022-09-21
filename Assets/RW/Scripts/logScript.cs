using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class logScript : MonoBehaviour
{

	public float rotationSpeed = 0.0f;
	private Collider2D logCollider;
	private SpriteRenderer logRenderer;
    // Start is called before the first frame update
    void Start()
    {
    	logCollider = gameObject.GetComponent<Collider2D>();
		logRenderer = gameObject.GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rotationSpeed < 1000){
            rotationSpeed = rotationSpeed + (float) 0.05;
        }
        transform.RotateAround(transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
