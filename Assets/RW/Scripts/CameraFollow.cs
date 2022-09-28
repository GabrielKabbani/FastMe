using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour{
    public GameObject targetObject;
    private float distanceToTarget;

    void Start(){
        distanceToTarget = transform.position.x - targetObject.transform.position.x;

    }

    void Update(){
        Vector3 newPosition = transform.position;
        newPosition.x = targetObject.transform.position.x + distanceToTarget;
        transform.position = newPosition;

        // float targetObjectX = targetObject.transform.position.x;
        // Vector3 newCameraPosition = transform.position;
        // newCameraPosition.x = targetObjectX + distanceToTarget;
        // transform.position = newCameraPosition;
    }
}
