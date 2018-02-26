using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoreWindow : MonoBehaviour
{
    [SerializeField] RectTransform ScrollViewContent;
    [SerializeField] RectTransform NoItems;
    [SerializeField] Button CloseButton;
    [SerializeField] Text txtCoins;
    private static bool bOpenStore;
    private Inventory inventario;

    //Singleton
    static System.Action OwnerCallback;

    private static StoreWindow _instance;
    public static StoreWindow Instance
    {
        get
        {
            if (!_instance)
            {
                //StoreWindow.LoadStore();
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
        CloseButton.onClick.AddListener(() => unloadStore());
        OwnerCallback.Invoke();
        inventario = GameManager.instance.getSimonSimon().GetInventory();
    }

    private void Update()
    {
        txtCoins.text = string.Format("{0}G", inventario.Coins.ToString());
    }

    /// <summary>
    /// inicialización serializada de objetos de la tienda
    /// </summary>
    /// <param name="items"></param>
    public void InitializeStore(System.Action<Item> onTryPurchaseItemCallBack, params Item[] items)
    {
        if (items.Length > 0)
        {
            NoItems.gameObject.SetActive(false);

            StoreItem reference = Resources.Load<StoreItem>("HUD/StoreItem");
            for (int i = 0; i < items.Length; i++)
            {
                Item CurrentItemStore = items[i];
                StoreItem instantiated = GameObject.Instantiate(reference);
                instantiated.Initialize(CurrentItemStore, onTryPurchaseItemCallBack);
                instantiated.transform.SetParent(ScrollViewContent, false);
                instantiated.transform.localScale = Vector3.one;

            }
        }
        else
        {
            NoItems.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Función que abre una escena aditiva de tienda
    /// </summary>
    /// <returns>El estado de la escena</returns>
    public static bool LoadStore(System.Action Callback)
    {
        OwnerCallback = Callback;
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
