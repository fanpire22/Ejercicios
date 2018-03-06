using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [Header("References")]
    [SerializeField] SimonBelmont _simonSimon;
    CheckPoint[] checkPoints;

    public SimonBelmont getSimonSimon()
    {
        return _simonSimon;
    }

    //SingleTonTORRON
    public static GameManager instance { get; private set; }

    private void Awake()
    {
        GameManager.instance = this;
        checkPoints = FindObjectsOfType<CheckPoint>();
    }

    public void ResetCheckPoints()
    {
        foreach(CheckPoint chkP in checkPoints)
        {
            chkP.Reset();
        }
    }
}
