using System;
using CodeBase.CardLogic;
using CodeBase.Factory;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase
{
    public class CardsLoader : MonoBehaviour
    {
        private const int MIN_RANDOM_INCLUSIVE = 4;
        private const int MAX_RANDOM_EXCLUSIVE = 7;
        
        public event Action<Card[]> OnCardsLoaded;
        
        [SerializeField] private Transform parent;

        private ICardFactory _cardFactory;
        private ISpriteProvider _spriteProvider;
        private ICardInfoLoader _cardInfoLoader;

        private void Start() => 
            LoadCards();

        private async void LoadCards()
        {
            _spriteProvider = new HttpSpriteProvider();
            _cardInfoLoader = new CardInfoLoader(_spriteProvider, GetCardsCount());
            _cardFactory = new CardFactory(await _cardInfoLoader.LoadCardInfo());

            var cards = _cardFactory.CreateCards(parent);
            
            OnCardsLoaded?.Invoke(cards);
        }

        private int GetCardsCount() => 
            Random.Range(MIN_RANDOM_INCLUSIVE, MAX_RANDOM_EXCLUSIVE);

        private void OnDestroy() => 
            _spriteProvider?.Dispose();
    }
}