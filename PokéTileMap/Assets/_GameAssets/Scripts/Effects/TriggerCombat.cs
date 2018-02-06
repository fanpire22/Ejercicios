using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCombat : MonoBehaviour {

    Pokemon[] pokemonList;
    GameManager _gameManager;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        pokemonList = new Pokemon[2];
        //{
            //Gengar
            //new Pokemon() { }
            //Ponyta
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CombatManager combatM = _gameManager.GetCombatManager();
        combatM.InitializeCombat(null);
        combatM.gameObject.SetActive(true);
    }
}
