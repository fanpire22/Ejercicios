using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIK : MonoBehaviour {

    Animator _animator;

    [SerializeField] Transform _target;
    [SerializeField] AvatarIKGoal _ikGoal;
    [SerializeField] float _ikAnimationDuration;

    float _weight = 0;
    bool _ikActive = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _ikActive = !_ikActive;
        }

        if(_ikActive)
        { 
            _weight += Time.deltaTime / _ikAnimationDuration;
            _weight = Mathf.Clamp01(_weight);
        }
        else
        {
            _weight -= Time.deltaTime / _ikAnimationDuration;
            _weight = Mathf.Clamp01(_weight);
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        _animator.SetIKPosition(_ikGoal, _target.position);
        _animator.SetIKPositionWeight(_ikGoal, _weight);
        _animator.SetIKRotation(_ikGoal, _target.rotation);
        _animator.SetIKRotationWeight(_ikGoal, _weight);
    }

}
