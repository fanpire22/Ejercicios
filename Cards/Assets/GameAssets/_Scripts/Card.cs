using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [Header("References")]
    public Text CostLabel;
    public Text Name;
    public Text Description;
    public Image Ilustration;

    public void Initialize(int cost, string name, string description, Sprite ilustration)
    {
        CostLabel.text = cost.ToString();
        Name.text = name;
        Description.text = description;
        Ilustration.sprite = ilustration;
    }
}
