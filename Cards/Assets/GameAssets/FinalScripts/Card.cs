using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FinalScript
{
    public class Card : MonoBehaviour
    {
        [SerializeField] bool _beginReverse;

        [HeaderAttribute("Style")]
        [SerializeField]
        Image _costImage;
        [SerializeField] Image _frameImage;

        [HeaderAttribute("Datos carta")]
        [SerializeField]
        Image _ilustration;
        [SerializeField] Text _titleLbl;
        [SerializeField] Text _descriptionLbl;
        [SerializeField] Text _costLbl;


        private float _beginMouseOverTime;
        private CardManager _cardManager;
        private bool _mouseOver;


        public void SetStyle(Sprite frame, Sprite cost, Material material)
        {
            _frameImage.sprite = frame;
            _frameImage.material = material;

            _costImage.sprite = cost;
        }

        public void Initialize(CardManager cardManager, Sprite ilustration, string title, string description, int cost)
        {
            _cardManager = cardManager;
            _ilustration.sprite = ilustration;
            _titleLbl.text = title;
            _descriptionLbl.text = description;
            _costLbl.text = cost.ToString();
        }

        public void Initialize(Card card)
        {
            this._titleLbl.text = card._titleLbl.text;
            this._descriptionLbl.text = card._descriptionLbl.text;
            this._ilustration.sprite = card._ilustration.sprite;
            this._costLbl.text = card._costLbl.text;
        }

        public void OnMouseOver(bool over)
        {
            print(over);
            _mouseOver = over;

            if (!over)
            {
                _beginMouseOverTime = -1;
                _cardManager.SetBigCardVisibility(false, null);
            }
        }

        // Use this for initialization
        void Awake()
        {
            _beginMouseOverTime = -1;
            if (_beginReverse)
            {
                GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 180, 0);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (_mouseOver)
            {
                if (_beginMouseOverTime < 0) _beginMouseOverTime = Time.time;
                else
                {
                    print(Time.time - _beginMouseOverTime);
                    if (Time.time - _beginMouseOverTime > _cardManager._delayShowBigCard)
                    {
                        _cardManager.SetBigCardVisibility(true, this);
                    }
                }
            }
        }
    }
}

