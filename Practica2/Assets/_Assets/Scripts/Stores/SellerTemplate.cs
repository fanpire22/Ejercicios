using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellerTemplate : MonoBehaviour
{
    bool bOpenStore;
    bool bInStore;
    public Item[] items;

    // Update is called once per frame
    void Update()
    {
        if (!bInStore) return;

        if (Input.GetKeyDown(KeyCode.Space) && !bOpenStore)
        {

            bOpenStore = StoreWindow.LoadStore(StoreLoaded);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && bOpenStore)
        {
            bOpenStore = StoreWindow.unloadStore();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bInStore = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bInStore = false;
        }
    }

    private void StoreLoaded()
    {
        Inventory inventario = GameManager.instance.getSimonSimon().GetInventory();
        StoreWindow.Instance.InitializeStore(inventario.Purchase, items);
    }

}
