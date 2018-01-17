using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour {

    public Pajaro jugador;
    public GameObject generador;
    public GameObject panel;
    public Cielo cielo;
    public AudioSource musGame;
    public AudioSource musTitle;


	// Use this for initialization
	void Awake ()
    {
        
        ActivacionObjetos(false);
	}

    /// <summary>
    /// Actualizaciones
    /// </summary>
    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        { 
            ActivacionObjetos(true);
        }
    }

    /// <summary>
    /// Función para inicializar la partida
    /// </summary>
    public void IniciarPartida()
    {
        ActivacionObjetos(true);
    }

    /// <summary>
    /// Función que determina si se deben activar o desactivar los componentes
    /// </summary>
    /// <param name="activar">True = Activar, False = Desactivar</param>
    private void ActivacionObjetos(bool activar)
    {
        jugador.enabled = activar;
        jugador.GetComponent<Rigidbody2D>().isKinematic = !activar;
        generador.SetActive(activar);
        panel.SetActive(!activar);
        cielo.enabled = activar;
        if (activar)
        {
            musGame.Play();
            musTitle.Stop();
        }
        else
        {
            musGame.Stop();
            musTitle.Play();
        }
    }
    
}
