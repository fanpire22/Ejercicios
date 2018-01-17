using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arma : MonoBehaviour {

    private int _danno = 30;
    [SerializeField] private int _municionActual = 8;
    [SerializeField] private int _cargador = 8;
    private float _fuerzaDisparo = 130;
    
    [SerializeField] private GameObject _posicionDisparo;
    [SerializeField] private GameObject _prefabProyectil;


    #region "Propiedades"

    public int Danno { get { return Danno; } }
    public int MunicionActual    { get { return _municionActual; } }
    public int Cargador { get { return _cargador; } }

    #endregion

    /// <summary>
    /// 
    /// </summary>
    private void Awake()
    {
        _municionActual = _cargador;
    }

    /// <summary>
    /// Función que dispara al objetivo del soldado
    /// </summary>
    public void Disparar()
    {
        if (_municionActual > 0)
        {
            //Creamos una bala en el lugar específico
            GameObject nuevaBala = Instantiate(_prefabProyectil);
            nuevaBala.transform.position = _posicionDisparo.transform.position;
            nuevaBala.transform.rotation = _posicionDisparo.transform.rotation;

            //movemos la bala por fuerza, en vez de velocidad constante
            nuevaBala.GetComponentInChildren<Rigidbody>().AddForce(_posicionDisparo.transform.forward * _fuerzaDisparo);
            //Le damos el daño del arma a la bala
            nuevaBala.GetComponentInChildren<Laser>().Danno = _danno;

            _municionActual -= 1;
            //objetivo.RecibirDisparo(danno);
        }
    }

    public void Recargar()
    {
        _municionActual = _cargador;
    }
}
