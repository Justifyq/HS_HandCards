using System.Collections.Generic;
using System.Linq;
using CodeBase.CardLogic;
using UnityEngine;

namespace CodeBase.UI
{
    public class CardsHandler : MonoBehaviour
    {
        [SerializeField] private CardsLoader loader;
        [SerializeField] private CardsAligner cardsAligner;
        [SerializeField] private CardRandomValueChanger valueChanger;

        private int _currentCardIndex;
        private List<Card> _loadedCards;

        private void Awake()
        {
            loader.OnCardsLoaded += CardsLoaded;
            valueChanger.OnValueChanged += SwitchCardOnChanger;
        }
        
        private void OnDestroy()
        {
            loader.OnCardsLoaded -= CardsLoaded;
            valueChanger.OnValueChanged -= SwitchCardOnChanger;
            
            foreach (var card in _loadedCards) 
                card.OnCardDestroyed -= CardDestroyed;
        }

        private void SwitchCardOnChanger()
        {
            if (_loadedCards.Count == 0)
                return;
            
            if (_currentCardIndex >= _loadedCards.Count)
                _currentCardIndex = 0;
            
            valueChanger.UpdateCard(_loadedCards[_currentCardIndex]);
            _currentCardIndex++;
        }
        
        private void CardsLoaded(Card[] loadedCards)
        {
            _loadedCards = loadedCards.ToList();
            cardsAligner.Align(_loadedCards);

            foreach (var card in _loadedCards) 
                card.OnCardDestroyed += CardDestroyed;

            SwitchCardOnChanger();
        }

        private void CardDestroyed(Card card)
        {
            card.OnCardDestroyed -= CardDestroyed;
            _loadedCards.Remove(card);
            cardsAligner.Align(_loadedCards);
        }
    }
}