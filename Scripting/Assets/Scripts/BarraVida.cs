using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour {

    [SerializeField] private GameObject _posicionParticula;
    [SerializeField] private ParticleSystem _prefabBarraVida;
    [SerializeField] private Image _barraVida;


    private int _vidaActual;
    private Soldado _padre;
    private Canvas _canvasPadre;
    private GameObject _camara;

    // Use this for initialization
    void Start () {

        _padre = GetComponentInParent<Soldado>();
        _vidaActual = _padre.VidaActual;
        _camara = GameObject.Find("Camara");
        _canvasPadre = GetComponentInParent<Canvas>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        _canvasPadre.transform.rotation = _camara.transform.rotation;

        if (_vidaActual != _padre.VidaActual)
        {
            //Hemos recibido daño, colocamos el spawner de partículas
            ParticleSystem nuevaParticula = Instantiate(_prefabBarraVida);
            nuevaParticula.transform.position = _posicionParticula.transform.position;
            nuevaParticula.transform.rotation = _posicionParticula.transform.rotation;

            _vidaActual = _padre.VidaActual;
            _barraVida.fillAmount = (float)_vidaActual / _padre.VidaMaxima;

            Destroy(nuevaParticula, 1f);
        }
	}
}
