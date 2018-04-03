using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesDuration : MonoBehaviour {

    Animator _animator;
    [SerializeField] ParticleSystem _particles;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    
	void Update () {
        float particlesDuration = _animator.GetFloat("particlesDuration");
                
        ParticleSystem.MainModule mainModule = _particles.main;
        mainModule.startLifetimeMultiplier = particlesDuration;
        
	}
}
