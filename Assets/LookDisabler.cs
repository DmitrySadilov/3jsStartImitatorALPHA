using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LookDisabler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private LookAround lookAround;

    private void Awake()
    {
        lookAround = FindObjectOfType<LookAround>();
    }

    public void OnPointerEnter( PointerEventData eventData)
    {
        lookAround.enabled = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        lookAround.enabled = true;
    }
}
