using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FinalScript
{
    public class CardManager : MonoBehaviour
    {
        [HeaderAttribute("Values")]
        public float _delayShowBigCard;
        public float _delayAddCard;

        [HeaderAttribute("Data")]
        [SerializeField]
        GameObject _cardPrefab;

        [HeaderAttribute("References")]
        [SerializeField]
        RectTransform _outputPanel;
        [SerializeField] Card _cardBig;

        [Space(10)]
        [HeaderAttribute("Ilustrations")]
        public Sprite[] _ilustrations;


        private string[] _names = new[] { "Cuchilla", "Haz de Poder", "Grito", "El Reto", "Llamas" };
        private string[] _descriptions = new[]
        {
        "Detiene todo ataque\n\n<i>No puedes pasar</i>"
        ,"Ganas defensa en el siguiente turno\n\n<i>Esperame...</i>"
        , "Tus enemigos pierden una vida\n\n<i>Eso duele</i>"
        , "Duplica tu reserva de cartas el siguiente turno\n\n<i>Ahora vereis</i>"
        , "Intercambia una carta con el jugador derecho\n\n<i>No es por molestar</i>"
    };

        public void GenerateCards()
        {
            StartCoroutine(IEAddCard(4));
        }

        public void SetBigCardVisibility(bool visible, Card card)
        {
            print("ACTIVANDO: " + visible);
            _cardBig.gameObject.SetActive(visible);

            if (visible) _cardBig.Initialize(card);

        }

        IEnumerator IEAddCard(int count)
        {
            var newCard = Instantiate(_cardPrefab);
            newCard.transform.SetParent(_outputPanel, false);

            Sprite ilustration = _ilustrations[Random.Range(0, _ilustrations.Length)];
            string title = _names[Random.Range(0, _names.Length)];
            string des = _descriptions[Random.Range(0, _descriptions.Length)];
            int cost = Random.Range(0, 20);

            newCard.GetComponent<Card>().Initialize(this, ilustration, title, des, cost);

            yield return new WaitForSeconds(_delayAddCard);

            if (count > 0)
            {
                StartCoroutine(IEAddCard(count - 1));
            }
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}


