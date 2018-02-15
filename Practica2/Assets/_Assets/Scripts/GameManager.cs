using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [Header("References")]
    [SerializeField] SimonBelmont _simonSimon;

    //SingleTonTORRON
    public static GameManager instance { get; private set; }

    private void Awake()
    {
        GameManager.instance = this;
    }
}
