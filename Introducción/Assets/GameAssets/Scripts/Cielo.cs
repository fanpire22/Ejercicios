using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cielo : MonoBehaviour {

    public float velocidad = 1;
    private Pajaro pajaro;

	// Use this for initialization
	void Start () {
        pajaro = GameObject.Find("Pajaro").GetComponent<Pajaro>();
	}
	
	// Update is called once per frame
	void Update () {
        //Hacemos que el cielo se mueva en horizontal
        if (!pajaro.Muerto)
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(velocidad * Time.time, 0);
	}
}
