using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CineMachineBeh : MonoBehaviour
{
    internal List<Transform> targetsToFilm = new List<Transform>();
    public List<GameObject> machines = new List<GameObject>();
    public List<Vector3> initialVectors = new List<Vector3>();
    public List<Quaternion> initialQuaternions = new List<Quaternion>();
    private List<Transform> points = new List<Transform>();

    private CinemachineBrain _brain;
    
    public int index = 0;

    public Button upBtn;
    public Button downBtn;

    private LookAround lookAround;
    private FloorsParser floorsParser;
    

    private void Awake()
    {
        lookAround = FindObjectOfType<LookAround>();
        floorsParser = FindObjectOfType<FloorsParser>();
        _brain = Camera.main.GetComponent<CinemachineBrain>();
       
        for (int i = 0; i < transform.childCount; i++)
        {
            points.Add(transform.GetChild(i).transform);
            machines.Add(transform.GetChild(i).gameObject);
            initialVectors.Add(transform.GetChild(i).position);
            initialQuaternions.Add(transform.GetChild(i).rotation);
        }
    }

    void Start()
    {
        targetsToFilm = floorsParser.floors;
        FindIntitialVector();
        SetCamera();
    }
    
    void Update()
    {
        if (index == 0) downBtn.interactable = false;
        else if (index == machines.Count - 1) upBtn.interactable = false;
        else
        {
            upBtn.interactable = true;
            downBtn.interactable = true;
        }
        
        if (Input.GetKeyDown(KeyCode.W))
            NextEntry();
        else if (Input.GetKeyDown(KeyCode.S))
            PrevEntry();
    }

    public void NextEntry()
    {
      
        StartCoroutine(Transit(index));
        _brain.enabled = true;
        index++;
        SetCamera();
    }

    public void PrevEntry()
    {
        
        StartCoroutine(Transit(index));
        
        _brain.enabled = true;
        index--;
        SetCamera();
    }

    private void SetCamera()
    {
        lookAround.target = targetsToFilm[index];
        lookAround.point = points[index];
        machines[index].SetActive(true);
    }

    private void OffCamera()
    {
        machines[index].transform.position = initialVectors[index];
        machines[index].transform.rotation = initialQuaternions[index];
    }

    private IEnumerator Transit(int ind)
    {
        OffCamera();
        lookAround.canLook = false;
        yield return new WaitForSeconds(_brain.m_DefaultBlend.m_Time);
       
        FindIntitialVector();
        machines[ind].SetActive(false);
        lookAround.canLook = true;
    }

    private void FindIntitialVector()
    {
        lookAround.directionInitial = lookAround.cam.transform.position - targetsToFilm[index].transform.position;
        lookAround.directionInitial = lookAround.directionInitial.normalized;
    }
}
