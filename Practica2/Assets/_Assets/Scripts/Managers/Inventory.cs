using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory : MonoBehaviour
{

    Dictionary<Item, int> Items;

    private int Gold;
    public int Coins{ get { return Gold; } private set { } }


    private void Awake()
    {
        Items = new Dictionary<Item, int>();
    }

    public bool CheckItems()
    {
        Items.OrderBy(i => i.Key.ID);
        for (int i = 0; i < Items.Count - 1; i++)
        {
            Item Actual = Items.ElementAt(i).Key;
            Item Siguiente = Items.ElementAt(i + 1).Key;
            if (Actual == Siguiente) return false;

        }
        return true;
    }
    public void AddGold(int amount)
    {
        Gold += Mathf.Max(0, amount);
    }

    public void RemoveGold(int amount)
    {
        if (amount < Gold) {
            Gold -= Mathf.Max(0, amount);
        }
        else
        {
            Gold = 0;
        }
    }

    public void Purchase(Item item)
    {
        if(Gold >= item.Cost)
        {
            RemoveGold(item.Cost);
            AddItem(item);
            item.ApplyEffects();
        }
    }

    /// <summary>
    /// Creamos un nuevo item en el diccionario si no existe. Si existe, añadimos N
    /// </summary>
    /// <param name="NewItem">Item a añadir</param>
    /// <param name="count">Número de items a añadir. Por defecto, 1</param>
    public void AddItem(Item NewItem, int count = 1)
    {
        if (!Items.ContainsKey(NewItem))
        {
            //Añadimos el item al inventario
            Items.Add(NewItem, count);
        }
        else
        {
            //Existe, así que añadimos uno más a su cantidad
            Items[NewItem] += count;
        }
    }

    /// <summary>
    /// Reducimos el número de objetos de un grupo de items, o bien borramos si reducimos más de los que tiene
    /// </summary>
    /// <param name="Item">Objeto a reducir</param>
    /// <param name="count">número de objetos que eliminar</param>
    public void RemoveItem(Item Item, int count = 1)
    {
        if (Items.ContainsKey(Item))
        {
            if (Items[Item] > count)
            {
                Items[Item] -= count;
            }
            else
            {
                Items.Remove(Item);
            }
        }
    }

    /// <summary>
    /// Reducimos el número de objetos de un grupo de items, o bien borramos si reducimos más de los que tiene
    /// </summary>
    /// <param name="Index">posición del objeto a reducir</param>
    /// <param name="count">número de objetos que eliminar</param>
    public void RemoveItemAt(int Index, int count = 1)
    {
        Item objeto = Items.Keys.ElementAt(Index);
        if (Items.Count > Index)
        {
            if (Items[objeto] > count)
            {
                Items[objeto] -= count;
            }
            else
            {
                Items.Remove(objeto);
            }
        }
    }

    public bool HasItem(Item ItemToCompare)
    {
        return Items.ContainsKey(ItemToCompare);
    }

    public bool HasItem(int Index)
    {
        if ((from x in Items.Keys where x.ID == Index select x).FirstOrDefault<Item>())
        {
            return true;
        }

        return false;
    }

    public Item GetItemAt(int Index)
    {
        if (Items.Count > Index)
        {
            return Items.ElementAt(Index).Key;
        }
        return null;
    }

    public Item GetItem(int Index)
    {
        return (from x in Items.Keys where x.ID == Index select x).FirstOrDefault<Item>();
    }
}
