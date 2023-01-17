using System.Collections.Generic;
using CodeBase.CardLogic;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.UI
{
    public class CardsAligner : MonoBehaviour
    {
        private const float ALIGN_INDEX_OFFSET = 1;

        [SerializeField] private float animDuration;
        [SerializeField] private float cardRotationZ;
        [SerializeField] private float cardOffsetX;
        [SerializeField] private float cardOffsetY;
        
        private readonly List<RectTransform> _cards = new List<RectTransform>();
        public void Align(IEnumerable<Card> cards)
        {
            _cards.Clear();
            
            foreach (var cardHandler in cards) 
                _cards.Add(cardHandler.GetComponent<RectTransform>());

            for (var i = 0; i < _cards.Count; i++)
                AlignByIndex(_cards[i], i, _cards.Count);
        }

        private void AlignByIndex(RectTransform card, int cardIndex, int cardCount)
        {
            var alignResult = cardIndex / (cardCount - ALIGN_INDEX_OFFSET);
            var rotZ = Mathf.Lerp(cardCount * cardRotationZ, cardCount * -cardRotationZ, alignResult);
            var xPos = Mathf.Lerp(cardCount * -cardOffsetX, cardCount * cardOffsetX, alignResult);
            var yPos = -Mathf.Abs(Mathf.Lerp(cardCount * -cardOffsetY, cardCount * cardOffsetY, alignResult));

            var pCurrent = card.anchoredPosition;
            var pTo = new Vector2(xPos, yPos);

            DOTween
                .To(() => pCurrent, x => pCurrent = x, pTo, animDuration)
                .SetEase(Ease.Linear)
                .OnUpdate(() => card.anchoredPosition = pCurrent);

            var rCurrent = card.rotation;
            var rTo = new Vector3(0,0, rotZ);
            
            DOTween
                .To(() => rCurrent, x => rCurrent = x, rTo, animDuration)
                .SetEase(Ease.Linear)
                .OnUpdate(() => card.rotation = rCurrent);
        }
    }
}