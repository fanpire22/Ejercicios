using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuElement : MonoBehaviour
{

    Image _image;
    Text _name;

    private void Awake()
    {
        _image = GetComponentInChildren<Image>();
        _name = GetComponentInChildren<Text>();
    }

    public void SetActive(bool isActive)
    {
        _image.enabled = isActive;
    }

    public void SetName(string Text)
    {
        _name.text = Text;
    }

}
