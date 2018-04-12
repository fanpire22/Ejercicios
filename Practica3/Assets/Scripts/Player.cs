using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : Soldier
{

    Projector _alertProjector;
    SphereCollider _alertCollider;

    protected override void Awake()
    {
        base.Awake();

        _alertProjector = GetComponentInChildren<Projector>();
        _alertCollider = _alertProjector.GetComponent<SphereCollider>();
    }

    protected override void Update()
    {
        base.Update();

        UpdateAlertArea();
        UpdateInput();
    }

    public void GoToDestination(Vector3 destination)
    {
        _agent.SetDestination(destination);
    }

    void UpdateAlertArea()
    {
        _alertProjector.orthographicSize = _agent.velocity.magnitude;
        _alertCollider.radius = _agent.velocity.magnitude;
    }

    void UpdateInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            _ani.SetTrigger("Crouch");
        }
    }
}
