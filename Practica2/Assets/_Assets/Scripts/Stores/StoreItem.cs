using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class StoreItem : MonoBehaviour
{
    [SerializeField] Image _image;
    [SerializeField] Text _name;
    [SerializeField] Button _buyButton;

    public void Initialize(Sprite image, string name, int cost /*,Delegado*/)
    {
        _image.sprite = image;
        _name.text = string.Format("{0} <color=yellow>{1}G</color>",name, cost); 
    }
}
