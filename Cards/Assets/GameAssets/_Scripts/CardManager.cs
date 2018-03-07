using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

[System.Serializable]
public struct FCardInfo
{
    public string Name;
    public string Description;
    public int Cost;
    public Sprite Ilustration;
}

public class CardManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Card _bigCard;
    [SerializeField] RectTransform _cardsStack;

    [Header("Prefabs")]
    [SerializeField] Card _cardPrefab;

    [Header("Values")]
    [SerializeField] FCardInfo[] CardInfo;

    void Start()
    {
        for(int i = 0; i < 5; i++)
        {
            PopulateCards();
        }
    }

    void PopulateCards()
    {
        Card cardInstantiated = GameObject.Instantiate(_cardPrefab);
        cardInstantiated.transform.SetParent(_cardsStack);

        FCardInfo rndInfo = CardInfo[Random.Range(0, CardInfo.Length)];
        cardInstantiated.Initialize
        (
            rndInfo.Cost,
            rndInfo.Name,
            rndInfo.Description,
            rndInfo.Ilustration
        );
    }
}
