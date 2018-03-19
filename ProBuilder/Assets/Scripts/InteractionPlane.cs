using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class InteractionPlane : MonoBehaviour
{
    NavMeshAgent _player;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>();
    }

    public void OnTerrainClicked(BaseEventData e)
    {
        PointerEventData pointerEvent = e as PointerEventData;
        Vector3 clickedPosition = pointerEvent.pointerCurrentRaycast.worldPosition;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _player.Warp(clickedPosition);
        }
        else
        {
            _player.SetDestination(clickedPosition);
        }
    }
}
