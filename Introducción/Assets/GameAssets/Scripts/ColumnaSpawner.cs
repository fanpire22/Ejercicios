using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnaSpawner : MonoBehaviour {

    //Altura superior máxima
    public float yMaximo = 5.6f;
    //Altura inferior mínima
    public float yMinimo = -1.3f;
    //Variación de espacio máxima entre columnas
    public float maxVariacionY = 1.5f;
    //Intervalo de spawneo entre columnas
    public float intervalo = 55;
    //Prefab de Spawner
    public GameObject prefabColumnas;
    //Variable global con el valor de la posición de la columna anterior
    private Vector3 posicionColumnaAnterior;
    //Jugador
    private Pajaro jugador;

	// Use this for initialization
	void Start ()
    {
        //obtenemos los objetos iniciales: la primera posición de columna, el estado del jugador y lanzamos el bucle de spawneo
        jugador = GameObject.Find("Pajaro").GetComponent<Pajaro>();
        posicionColumnaAnterior = prefabColumnas.transform.position;
        InvokeRepeating("Spawn", Time.deltaTime, Time.deltaTime * intervalo);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Función que spawnea columnas
    /// </summary>
    void Spawn()
    {
        if (!jugador.Muerto)
        {
            //Determinamos las variaciones máximas y mínimas que vamos a permitir en vertical de la columna respecto a la anterior. Por supuesto, no podemos salirnos del límite
            GameObject nuevaColumna = Instantiate(prefabColumnas);
            float yVariacionMax = posicionColumnaAnterior.y + maxVariacionY;
            float yVariacionMin = posicionColumnaAnterior.y - maxVariacionY;

            //Si con la variación íbamos a salirnos, nos quedamos en el límite ya sea superior o inferior
            if (yVariacionMax > yMaximo)
                yVariacionMax = yMaximo;

            if (yVariacionMin < yMinimo)
                yVariacionMin = yMinimo;

            //Asignamos las posiciones con una posición vertical determinada al azar dentro de la variación de columnas
            Vector3 posicion = new Vector3(prefabColumnas.transform.position.x + Random.Range(-0.5f,0.5f), Random.Range(yVariacionMin, yVariacionMax), prefabColumnas.transform.position.z);
            nuevaColumna.transform.position = posicion;
            posicionColumnaAnterior = posicion;

        }else{
            //Cancelamos la generación de columnas
            CancelInvoke();
        }
    }
}
