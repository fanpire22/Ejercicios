using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDLifeBar : MonoBehaviour {

    [SerializeField] private Image _foreground;

    public void SetValue(float value)
    {
        _foreground.fillAmount = value;
    }

    public void ActAmmo(int ammo)
    {

    }

}
