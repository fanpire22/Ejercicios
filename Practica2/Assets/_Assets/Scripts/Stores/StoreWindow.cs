using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoreWindow : MonoBehaviour
{
    [System.Serializable]
    public struct FStoreItem
    {
        Sprite sprite;
        string name;
        int amount;
    }

    static bool bOpenStore;

    //Singleton
    private static StoreWindow _instance;
    public static StoreWindow Instance
    {
        get
        {
            if (_instance == null)
            {
                StoreWindow.LoadStore();
                return _instance;
            }
            return _instance;
        }
        private set
        {
            _instance = value;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    /// <summary>
    /// Función que abre una escena aditiva de tienda
    /// </summary>
    /// <returns>El estado de la escena</returns>
    public static bool LoadStore()
    {
        if (!bOpenStore)
        {
            SceneManager.LoadScene("Store", LoadSceneMode.Additive);
            bOpenStore = true;
        }
        return bOpenStore;
    }

    /// <summary>
    /// Función que cierra una escena aditiva de tienda
    /// </summary>
    /// <returns></returns>
    public static bool unloadStore()
    {
        if (bOpenStore)
        {
            _instance.StartCoroutine(_instance.unloadStore_Corrutine());
            bOpenStore = false;
        }
        return bOpenStore;
    }

    public IEnumerator unloadStore_Corrutine()
    {
        AsyncOperation op = SceneManager.UnloadSceneAsync("Store");
        yield return op;
    }

}
