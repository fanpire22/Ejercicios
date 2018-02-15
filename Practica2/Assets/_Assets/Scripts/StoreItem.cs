using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreItem : MonoBehaviour {

    [SerializeField] Image _icon;
    [SerializeField] Text _name;
    [SerializeField] Text _cost;
    [SerializeField] Button _buyButton;

    public void Initialize(Sprite Image, string Name, int Cost)
    {
        _icon.sprite = Image;
        _name.text = Name;
        _name.text = string.Format("{0}G",Cost);
    }

}
