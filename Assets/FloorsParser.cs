using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorsParser : MonoBehaviour
{
    internal List<Transform> floors = new List<Transform>();

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            floors.Add(transform.GetChild(i).transform);
        }
    }
}
