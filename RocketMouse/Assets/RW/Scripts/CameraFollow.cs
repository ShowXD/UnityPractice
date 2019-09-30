using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private float distanceToTarget;

    public GameObject targetObject;

    // Start is called before the first frame update
    void Start()
    {
        distanceToTarget = transform.position.x - targetObject.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        // Setting the Camera position of x
        float targetObjectX = targetObject.transform.position.x;
        Vector3 newCameraPosition = transform.position;
        newCameraPosition.x = targetObjectX + distanceToTarget;
        transform.position = newCameraPosition;
    }
}
