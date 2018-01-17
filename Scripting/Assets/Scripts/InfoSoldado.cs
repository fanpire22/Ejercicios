using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoSoldado : MonoBehaviour {

    [SerializeField] private Soldado soldadoObservado;
    private string _formatoTexto = "Nombre: {0}\nVida:{1}/{2}\nArma:{3}/{4}";
    private Text _texto;

    // Use this for initialization
    private void Start()
    {
        _texto = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update () {
        if (soldadoObservado != null)
        {
            _texto.text = string.Format(_formatoTexto, soldadoObservado.Nombre, soldadoObservado.VidaActual, soldadoObservado.VidaMaxima, soldadoObservado.Arma.MunicionActual, soldadoObservado.Arma.Cargador);
        }
        else
        {
            _texto.text = "MUERTO";
        }
	}
}
