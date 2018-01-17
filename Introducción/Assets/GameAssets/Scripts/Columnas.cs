using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Columnas : MonoBehaviour {

    public float velocidad = 200;
    private Pajaro jugador;
    private bool puedoDestruirme = false;
    private SpriteRenderer render;

	// Use this for initialization
	void Start () {

        jugador = GameObject.Find("Pajaro").GetComponent<Pajaro>();
        render = GetComponentInChildren<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!jugador.Muerto) {
            //Movemos las columnas a una velocidad constante
            transform.position = transform.position - Vector3.right * Time.deltaTime * velocidad;

            if(render.isVisible)
            {
                puedoDestruirme = true;
            }
            else if (puedoDestruirme)
            {
                Destroy(this.gameObject);
            }
        }

    }
}
