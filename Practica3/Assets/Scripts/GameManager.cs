using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    static GameManager _instance;
    public static GameManager GetInstance
    {
        get { return _instance; }
    }

    public Player player
    {
        get; private set;
    }

    private void Awake()
    {
        player = FindObjectOfType<Player>();

        _instance = this;
    }

}
