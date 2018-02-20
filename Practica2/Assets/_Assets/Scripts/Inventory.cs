using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    Dictionary<Item, int> Items;
    private void Awake()
    {
        Items = new Dictionary<Item, int>();
    }

    public void AddItem(Item NewItem)
    {
        if (!Items.ContainsKey(NewItem))
        {
            //Añadimos el item al inventario
            Items.Add(NewItem, 1);
        }
        else
        {
            //Existe, así que añadimos uno más a su cantidad
            Items[NewItem]++;
        }
    }

    public void RemoveItem(Item ItemToRemove)
    {
        Items.Remove(ItemToRemove);
    }

    public bool HasItem(Item ItemToCompare)
    {
            return Items.ContainsKey(ItemToCompare);
    }

    public Item GetItem(int Index)
    {
        if (Items.Count > Index)
        {
        }
        return null;
    }
}
