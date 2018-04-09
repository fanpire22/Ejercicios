using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Floor : MonoBehaviour, IPointerClickHandler, IDragHandler
{
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 clickedPoint = eventData.pointerCurrentRaycast.worldPosition;

        GameManager.GetInstance.player.GoToDestination(clickedPoint);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Vector3 clickedPoint = eventData.pointerCurrentRaycast.worldPosition;

        GameManager.GetInstance.player.GoToDestination(clickedPoint);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
