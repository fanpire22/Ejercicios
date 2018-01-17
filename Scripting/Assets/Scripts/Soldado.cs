using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldado : MonoBehaviour {

    private int _vidaActual;
    private float _precision = 0.5f;
    private string _nombre;
    [SerializeField] private int _vidaMaxima = 100;
    [SerializeField] private GameObject _prefabMuerte;

    private Arma fusilEstandar;
    //public Arma fusilHealer;
    private Renderer renderizador;

    #region "Propiedades"
    public int VidaActual { get { return _vidaActual; } }
    public float Precision { get { return _precision; } }
    public int VidaMaxima { get { return _vidaMaxima; } }
    public Arma Arma { get { return fusilEstandar; } }

    public string Nombre
    {
        get
        {
            return _nombre;
        }
        set
        {
            _nombre = value;
        }
    }


    #endregion

    private void Awake()
    {
        _vidaActual = _vidaMaxima;

        fusilEstandar = this.GetComponentInChildren<Arma>();

        //Determinamos al azar la precisión de nuestro personaje (la variación en espacio respecto a los disparos que vaya a hacer)
        _precision = Random.Range(0f, 1.5f);

        //Le asignamos una copia del material de nuestro color, y le ponemos un color al azar
        renderizador = GetComponent<Renderer>();
        renderizador.material = new Material(renderizador.material);

        renderizador.material.color = Random.ColorHSV(0,1,0.8f,0.8f,1,1); 
    }

    private void Update()
    {
        if (Arma.MunicionActual == 0)
        {
            print(string.Format("{0} dice: ¡Recargando!",_nombre));
            Invoke("Recargar", 1f);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void Disparar(Soldado enemigo, Camera camara)
    {
        if (enemigo != null && Arma.MunicionActual > 0)
        {
            //Nos giramos para apuntar al enemigo, y le encañonamos para disparar. Ponemos en juego la precisión del soldado
            Vector3 posicion = enemigo.transform.position + new Vector3(Random.Range(_precision*-1,_precision), Random.Range(_precision * -1, _precision), Random.Range(_precision * -1, _precision));
            transform.LookAt(posicion);
            Arma.transform.LookAt(posicion);

            camara.transform.position = transform.position;
            camara.transform.LookAt(enemigo.transform.position);
            camara.transform.Translate(Vector3.back * 5);
            camara.transform.Translate(Vector3.up*2);

            Debug.Log(string.Format("{0} dice: ¡Disparo a Enemigo {1}!", _nombre, enemigo._nombre));
            fusilEstandar.Disparar();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void RecibirDisparo(int danno)
    {
        _vidaActual -= danno;
        if(_vidaActual < 1)
        {
            CancelInvoke();
            Debug.Log(string.Format("{0} dice: ¡AAAARGGHHHH!", _nombre));
            Destroy(this.gameObject);

            GameObject nuevaExplosion = Instantiate(_prefabMuerte);
            nuevaExplosion.transform.position = this.transform.position;
            Destroy(nuevaExplosion, 1);
        }
        else
        {
            Debug.Log(string.Format("{0} dice: ¡Me han dado! ¡Sólo me queda {1} de vida!", _nombre, _vidaActual));
        }
    }

    private void Recargar()
    {
        print(string.Format("{0} dice: ¡Listo!", _nombre));
        Arma.Recargar();
    }


}
