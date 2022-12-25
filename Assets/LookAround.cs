using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class LookAround : MonoBehaviour
{
    private Camera cam;
    internal Transform target; 
    internal Transform point;

    [SerializeField] [Range(0, 360)] private int maxRotationInOneSwipe = 180;

    private CinemachineBrain _brain;

    private Vector3 previousPosition;

    public bool canLook = true;

    private void Start()
    {
        cam = Camera.main;
        _brain = cam.GetComponent<CinemachineBrain>();
    }

    void Update()
    {
        if (canLook)
        {
            if (Input.GetMouseButtonDown(0))
            {
                previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
            }
            else if (Input.GetMouseButton(0))
            {
                _brain.enabled = false;
                Vector3 newPosition = cam.ScreenToViewportPoint(Input.mousePosition);
                Vector3 direction = previousPosition - newPosition;

                float rotationAroundYAxis = -direction.x * maxRotationInOneSwipe; 
                float rotationAroundXAxis = direction.y * maxRotationInOneSwipe; 

                cam.transform.position = target.position;

                cam.transform.Rotate(new Vector3(1, 0, 0), rotationAroundXAxis);
                cam.transform.Rotate(new Vector3(0, 1, 0), rotationAroundYAxis,
                    Space.World); 

                cam.transform.Translate(new Vector3(0, 0, -Vector3.Distance(target.transform.position, point.transform.position)));

                previousPosition = newPosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                point.position = cam.transform.position;
                point.rotation = cam.transform.rotation;
                _brain.enabled = true;
            }
            
        }

    }
}
