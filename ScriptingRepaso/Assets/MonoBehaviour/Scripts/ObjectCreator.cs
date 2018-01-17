using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCreator : MonoBehaviour {

    [SerializeField] private GameObject _prefab;

	// Use this for initialization
	void Start () {

        GameObject newGO = Instantiate(_prefab);
        newGO.name = "Fulanitroll";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
