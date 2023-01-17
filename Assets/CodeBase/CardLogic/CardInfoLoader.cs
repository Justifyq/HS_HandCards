using System.Threading.Tasks;
using CodeBase.Data;
using CodeBase.Factory;
using UnityEngine;

namespace CodeBase.CardLogic
{
    public class CardInfoLoader : ICardInfoLoader
    {
        private const int MIN_RANDOM = 1;
        private const int MAX_RANDOM = 10;
        private const string DESCRIPTION_FORMAT = "Card generated from https://picsum.photos/ with seed {0}";
    
        private readonly int _cardCount;
        private readonly ISpriteProvider _spriteProvider;
        
        public CardInfoLoader(ISpriteProvider spriteProvider, int cardCount)
        {
            _spriteProvider = spriteProvider;
            _cardCount = cardCount;
        }

        public async Task<CardInfo[]> LoadCardInfo()
        {
            CardInfo[] cardsInfos = new CardInfo[_cardCount];

            for (int i = 0; i < _cardCount; i++)
            {
                var cardSeed = await _spriteProvider.LoadSprite();
                cardsInfos[i] = GenerateCard(cardSeed);
            }

            return cardsInfos;
        }

        private CardInfo GenerateCard(SpriteSeed cardSeed)
        {
            return new CardInfo(
                cardSeed.Sprite, 
                cardSeed.Seed,
                string.Format(DESCRIPTION_FORMAT, cardSeed.Seed), 
                GenerateRandomValue(),
                GenerateRandomValue(),
                GenerateRandomValue());
        }

        private int GenerateRandomValue() => 
            Random.Range(MIN_RANDOM, MAX_RANDOM);
    }
}