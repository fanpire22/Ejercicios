using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField] Soldado _prefabSoldier;
    [SerializeField] float _spawnRadius = 5;
    [SerializeField] int _soldierNum = 12;
    [SerializeField] Camera camara;

    GameObject[] _soldiers;

    private string[] _soldierName = {"Kenny", "Peter", "John", "Luke", "Ryan", "Bryan", "Arnold", "Paco", "Leeroy JENKINS", "Grunt", "Sylvanas","Chaqueta Roja", "Arthas"};

	// Use this for initialization
	void Start () {
        SpawnSoldiers();

        InvokeRepeating("SoldierShoot", 3f, 2f);
    }
	
	// Update is called once per frame
	void Update () {
	}

    /// <summary>
    /// Función que genera soldados
    /// </summary>
    void SpawnSoldiers()
    {
        float angulo = 0;
        //Bucle para ir generando soldados
        for (int i = 0; i < _soldierNum; i++)
        {
            Soldado newSoldier = Instantiate(_prefabSoldier);

            newSoldier.Nombre = _soldierName[Random.Range(0, _soldierName.Length)];
            newSoldier.name = string.Format("{0} ({1})",newSoldier.Nombre,newSoldier.Precision);

            newSoldier.transform.position = new Vector3(Mathf.Sin(angulo * Mathf.Deg2Rad) * _spawnRadius, 1, Mathf.Cos(angulo * Mathf.Deg2Rad) * _spawnRadius);
            newSoldier.transform.LookAt(new Vector3(0, 1, 0));

            angulo += 360.0f / _soldierNum;
        }
    }

    /// <summary>
    /// Función que hace que un soldado dispare a otro soldado
    /// </summary>
    void SoldierShoot()
    {
        _soldiers = GameObject.FindGameObjectsWithTag("Soldado");

        if (_soldiers.Length > 1)
        {
            int randomAttackerIndex = Random.Range(0, _soldiers.Length);
            int randomDefenderIndex = Random.Range(0, _soldiers.Length);

            //Recalculamos el índice si es igual al del atacante: No nos interesa que los soldados se disparen en el pie
            while ((randomDefenderIndex == randomAttackerIndex) && _soldiers.Length > 1)
            {
                randomDefenderIndex = Random.Range(0, _soldiers.Length);
            }

            //Giramos al soldado atacante para que mire al defensor, y que le dispare
            Soldado attacker = _soldiers[randomAttackerIndex].GetComponent<Soldado>();
            Soldado defender = _soldiers[randomDefenderIndex].GetComponent<Soldado>();
            
            attacker.Disparar(defender, camara);
        }
        else
        {
            Debug.Log(string.Format("EL GANADOR ES: {0}", _soldiers[0].name));
            CancelInvoke();
        }
    }


}
