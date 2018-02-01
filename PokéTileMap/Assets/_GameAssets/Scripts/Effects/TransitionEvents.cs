using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionEvents : MonoBehaviour {

    GameManager _gameManager;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    /// <summary>
    /// Primer rellenado
    /// </summary>
    public void OnFirstFillComplete()
    {
        _gameManager.OnFinishedAnimation();
    }

    public void OnAnimationComplete()
    {

    }

}
