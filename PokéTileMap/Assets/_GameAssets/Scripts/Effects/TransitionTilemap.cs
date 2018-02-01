using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionTilemap : MonoBehaviour {

    [SerializeField] GameObject _destinyTransition;

    GameManager _gameManager;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _gameManager.ShowTransition(this);
            _gameManager.CharacterRef.enabled = false;
        }
    }

    public void OnTransitionEnded()
    {
        _destinyTransition.SetActive(true);
        transform.root.gameObject.SetActive(false);
        _gameManager.CharacterRef.enabled = true;
    }
}
