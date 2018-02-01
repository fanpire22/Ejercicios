using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusInfo : MonoBehaviour {

    [SerializeField] Text _name;
    [SerializeField] Text _currentLife;
    [SerializeField] Text _maxLife;
    [SerializeField] Text _level;
    [SerializeField] Image _HealthBar;

    Pokemon _currentPokemon;

    public void Initialize(Pokemon pokemon)
    {
        _currentPokemon = pokemon;
    }

    public void UpdateHUD()
    {

    }
}
