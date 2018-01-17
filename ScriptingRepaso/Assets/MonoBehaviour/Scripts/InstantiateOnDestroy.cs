using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateOnDestroy : MonoBehaviour {

    [SerializeField] private GameObject _prefab;
    private bool isQuitting = false;

    private void OnApplicationQuit()
    {
        isQuitting = true;
    }

    private void OnDestroy()
    {
        if (!isQuitting)
        {
            GameObject objeto =  Instantiate(_prefab);
            objeto.SetActive(true);
        }
    }

}
