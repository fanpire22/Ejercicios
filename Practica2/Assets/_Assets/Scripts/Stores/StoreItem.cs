using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class StoreItem : MonoBehaviour
{
    [SerializeField] Image _image;
    [SerializeField] Text _name;
    [SerializeField] Button _buyButton;

    Item _reference;

    public void Initialize(Item reference, System.Action<Item> callback)
    {
        _image.sprite = reference.Icon;
        _name.text = string.Format("{0} <color=yellow>{1}G</color>",reference.Name, reference.Cost);
        _reference = reference;

        _buyButton.onClick.AddListener(()=> callback.Invoke(_reference));
    }
}
