using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puntuacion : MonoBehaviour {

    private Pajaro jugador;
    private Text texto;

	// Use this for initialization
	void Start () {
        jugador = GameObject.Find("Pajaro").GetComponent<Pajaro>();
        texto = GetComponent<Text>();

    }
	
	// Update is called once per frame
	void Update () {
        if (!jugador.Muerto)
        texto.text = jugador.Puntos.ToString();

	}
}
