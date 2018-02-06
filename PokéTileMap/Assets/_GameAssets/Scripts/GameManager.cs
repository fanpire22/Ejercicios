using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField] TransitionEvents _transitionE;
    [SerializeField] CombatManager _combatManager;

    public CharacterMovement CharacterRef { private set; get; }

    Animator _radialTransitionAnimator;
    TransitionTilemap _currentTransitionT;

    private void Awake()
    {
        _radialTransitionAnimator = _transitionE.GetComponent<Animator>();
        CharacterRef = FindObjectOfType<CharacterMovement>();
    }

    public void ShowTransition(TransitionTilemap caller)
    {
        _currentTransitionT = caller;
        _radialTransitionAnimator.SetTrigger("inicializar");
    }

    public void OnFinishedAnimation()
    {
        _currentTransitionT.OnTransitionEnded();
    }

    public CombatManager GetCombatManager()
    {
        return _combatManager;
    }
}
