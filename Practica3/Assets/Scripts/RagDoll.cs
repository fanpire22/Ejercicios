using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagDoll : MonoBehaviour {

    Animator _ani;
    private Rigidbody[] _rigs;

    private void Awake()
    {
        _ani = GetComponent<Animator>();
        _rigs = GetComponentsInChildren<Rigidbody>();
    }

    public void ActivateRagDoll()
    {
        _ani.enabled = false;
        for(int i = 0; i< _rigs.Length; i++)
        {
            _rigs[i].isKinematic = false;
        }
    }

}
