using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellerTemplate : MonoBehaviour
{
    bool bOpenStore;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !bOpenStore)
        {
            bOpenStore = StoreWindow.LoadStore();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && bOpenStore)
        {
            bOpenStore = StoreWindow.unloadStore();
        }
    }
}
