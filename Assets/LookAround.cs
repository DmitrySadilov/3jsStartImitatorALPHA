using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class LookAround : MonoBehaviour
{
    internal Camera cam;
    internal Transform target; 
    internal Transform point;

    [SerializeField]  private float maxRotationInOneSwipe = 180f;

    private CinemachineBrain _brain;

    private Vector3 previousPosition;

    public bool canLook = true;

    private CineMachineBeh _cineMachineBeh;

    [Header("FOV Settings")]
    [SerializeField] float mutateSpeed;
    [SerializeField] float bias;
    [SerializeField] float power;
    internal Vector3 directionInitial;


    private void Awake()
    {
        _cineMachineBeh = FindObjectOfType<CineMachineBeh>();
        cam = Camera.main;
        _brain = cam.GetComponent<CinemachineBrain>();
    }
    private void Start()
    {

        

    }

    void Update()
    {
        Debug.LogWarning(directionInitial);
        Debug.Log(cam.transform.forward);
        if (canLook)
        {
            if (Input.GetMouseButtonDown(0))
            {
                previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
            }
            else if (Input.GetMouseButton(0))
            {
                Look();
            }
            else if (Input.GetMouseButtonUp(0))
            {
               StopLook();
            }
            
        }

    }

    private void Look()
    {
        _brain.enabled = false;
        // Vector3 vectorToObject = (currentObj.position - cam.transform.position).normalized;
        // Debug.LogError(vectorToObject);
        // Debug.Log(cam.transform.forward);
        // float dot = Vector3.Dot(cam.transform.forward, vectorToObject) - bias;
        float dot = Vector3.Dot(-cam.transform.forward, directionInitial) - bias;
        //Debug.Log(dot);
        dot = Mathf.Pow(dot, power);
        mutateSpeed = Mathf.Max(maxRotationInOneSwipe * dot, 0.1f);

        Vector3 newPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        Vector3 direction = previousPosition - newPosition;

        float rotationAroundYAxis = -direction.x * mutateSpeed;
        float rotationAroundXAxis = direction.y * mutateSpeed;



        cam.transform.position = target.position;
        cam.transform.Rotate(new Vector3(1, 0, 0), rotationAroundXAxis);
        cam.transform.Rotate(new Vector3(0, 1, 0), rotationAroundYAxis, Space.World);



        cam.transform.Translate(new Vector3(0, 0,
            -Vector3.Distance(target.transform.position, point.transform.position)));


        previousPosition = newPosition;
    }

    private void StopLook()
    {
        point.position = cam.transform.position;
        point.rotation = cam.transform.rotation;
        _brain.enabled = true;
    }

   
}
