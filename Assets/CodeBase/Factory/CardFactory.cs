using CodeBase.CardLogic;
using CodeBase.Data;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Factory
{
    public class CardFactory : ICardFactory
    {
        private const string CARD_PREFAB_PATH = "Prefabs/Card";
        
        private readonly Card _cardPrefab;
        private readonly CardInfo[] _cardsInfos;
        private readonly Card[] _cards;

        private bool isCreated;

        public CardFactory(CardInfo[] cards)
        {
            _cardPrefab = Resources.Load<Card>(CARD_PREFAB_PATH);
            _cardsInfos = cards;
            _cards = new Card[_cardsInfos.Length];
        }

        public Card[] CreateCards(Transform parent = null)
        {
            if (isCreated)
                return _cards;
        
            for (var i = 0; i < _cardsInfos.Length; i++) 
                _cards[i] = CreateCard(_cardsInfos[i], parent);
        
            isCreated = true;
        
            return _cards;
        }

        private Card CreateCard(CardInfo cardInfo, Transform parent)
        {
            var card = parent == null ? Object.Instantiate(_cardPrefab) : Object.Instantiate(_cardPrefab, parent);
            var view = card.GetComponent<CardView>();

            card.InitCard(
                cardInfo.ManaCost, 
                cardInfo.HealthValue, 
                cardInfo.DamageValue,
                view);
        
            view.InitCardView(
                cardInfo.CardSprite, 
                cardInfo.CardName, 
                cardInfo.Description, 
                cardInfo.ManaCost,
                cardInfo.DamageValue, 
                cardInfo.HealthValue);

            return card;
        }
    }
}