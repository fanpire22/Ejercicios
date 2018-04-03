using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollControl : MonoBehaviour {

    [SerializeField]  bool _activateRootRigidBody = true;

    Animator _animator;

    // Rigidibodies y colliders de las partes del cuerpo
    // En la posición 0 podría estar o no estar el rigibody y collider del objeto raíz
    Rigidbody[] _ragdollRigidbodies;
    Collider[] _ragdollColliders;

    // Rigidbody y collider del objeto raíz
    Rigidbody _rootRigidbody;
    Collider _rootCollider;

    // Bool para controlar si el ragdoll está activo 
    bool _ragdollOn = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rootCollider = GetComponent<Collider>();
        _rootRigidbody = GetComponent<Rigidbody>();

        _ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        _ragdollColliders = GetComponentsInChildren<Collider>();
    }

    private void Start()
    {
        DeactivateRagdoll();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if(_ragdollOn)
            {
                DeactivateRagdoll();
            }
            else
            {
                ActivateRagdoll();
            }
        }
    }

    public void ActivateRagdoll()
    {
        // Activo el booleano
        _ragdollOn = true;

        // Desactivo las animaciones
        _animator.enabled = false;
        
        // Activo las físicas de todos los rigidbodies
        for(int i=0; i<_ragdollRigidbodies.Length; i++)
        {
            _ragdollRigidbodies[i].isKinematic = false;
        }        

        // Activo todos los colliders
        for(int i=0; i<_ragdollColliders.Length; i++)
        {
            _ragdollColliders[i].enabled = true;
        }

        // Desactivo las físicas del rigidbody raíz
        if (_rootRigidbody != null) { _rootRigidbody.isKinematic = true; }

        // Desactivo el collider raíz para que las partes 
        // del cuerpo no colisionen contra la cápsula
        if (_rootCollider != null) { _rootCollider.enabled = false; }
    }

    public void DeactivateRagdoll()
    {
        // Desactivo el booleano
        _ragdollOn = false;

        // Activo las animaciones
        _animator.enabled = true;
               
        // Desactivo las físicas de todos los rigidbodies
        for (int i = 0; i < _ragdollRigidbodies.Length; i++)
        {
            _ragdollRigidbodies[i].isKinematic = true;
        }

        // Desactivo todos los colliders
        for (int i = 0; i < _ragdollColliders.Length; i++)
        {
            _ragdollColliders[i].enabled = false;
        }

        // Activo las físicas del rigidbody raíz
        if (_rootRigidbody != null) { _rootRigidbody.isKinematic = !_activateRootRigidBody; }

        // Activo el collider raíz
        if (_rootCollider != null) { _rootCollider.enabled = true; }
    }
}
