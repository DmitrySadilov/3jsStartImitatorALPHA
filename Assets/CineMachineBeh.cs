using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CineMachineBeh : MonoBehaviour
{
    public List<Transform> targetsToFilm = new List<Transform>();
    private List<GameObject> machines = new List<GameObject>();
    private List<Transform> initialPoses = new List<Transform>();
    public List<Transform> points = new List<Transform>();

    private CinemachineBrain _brain;
    
    private int index = 0;

    public Button upBtn;
    public Button downBtn;

    private LookAround lookAround;
    

    private void Awake()
    {
        _brain = Camera.main.GetComponent<CinemachineBrain>();
        lookAround = FindObjectOfType<LookAround>();
        for (int i = 0; i < transform.childCount; i++)
        {
            machines.Add(transform.GetChild(i).gameObject);
            initialPoses.Add(transform.GetChild(i).transform);
        }
    }

    void Start()
    {
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
        OffCamera();
        index++;
        SetCamera();
    }

    public void PrevEntry()
    {
        StartCoroutine(Transit(index));
        _brain.enabled = true;
        OffCamera();
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
        //machines[index].SetActive(false);
        machines[index].transform.position = initialPoses[index].transform.position;
        machines[index].transform.rotation = initialPoses[index].transform.rotation;
    }

    private IEnumerator Transit(int ind)
    {
        lookAround.canLook = false;
        //lookAround.enabled = false;
        yield return new WaitForSeconds(_brain.m_DefaultBlend.m_Time);
        machines[ind].SetActive(false);
        //lookAround.enabled = true;
        lookAround.canLook = true;
    }
}
