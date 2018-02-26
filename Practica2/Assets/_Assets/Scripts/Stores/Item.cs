using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Objeto que no es un objeto de mundo, si no de información (de ahí que sea AssetMenu y no MonoBehaviour). Es más eficiente en memoria
/// </summary>
[CreateAssetMenu(fileName = "Create Item", menuName = "Items", order = 0)]
public class Item : ScriptableObject {

    public int ID;
    public Sprite Icon;
    public string Name;
    public int Cost;

    public static bool operator ==(Item a, Item b)
    {
        return a.ID == b.ID;
    }

    public static bool operator !=(Item a, Item b)
    {
        return a.ID != b.ID;
    }

    public virtual void ApplyEffects()
    {

    }
}
