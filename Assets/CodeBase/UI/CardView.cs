using System;
using CodeBase.CardLogic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public interface ICardView
    {
        void ChangeManaView(int from, int to);
        
        void ChangeHealthView(int from, int to, Action onComplete = null);
        void ChangeDamageView(int from, int to);
    }

    public class CardView : MonoBehaviour, ICardView
    {
        [SerializeField] private Card _card;
        [SerializeField] private Text _healthView;
        [SerializeField] private Text _damageView;
        [SerializeField] private Text _manaView;
        [SerializeField] private Image _cardImage;
        [SerializeField] private Text _descriptionView;
        [SerializeField] private Text _cardNameView;
        [SerializeField] private float perUnitDelay;

        public void InitCardView(Sprite cardSprite, string cardName, string description, int mana, int damage, int health)
        {
            _cardImage.sprite = cardSprite;
            _manaView.text = mana.ToString();
            _damageView.text = damage.ToString();
            _healthView.text = health.ToString();
            _descriptionView.text = description;
            _cardNameView.text = cardName;
        }

        public void ChangeManaView(int from, int to) => 
            ChangeViewAnimation(_manaView, from, to);

        public void ChangeHealthView(int from, int to, Action onComplete = null) => 
            ChangeViewAnimation(_healthView, from, to, onComplete);

        public void ChangeDamageView(int from, int to) => 
            ChangeViewAnimation(_damageView, from, to);

        private void ChangeViewAnimation(Text view, int from, int to, Action onComplete = null)
        {
            var current = from;
            var change = to - current > 0 ?  to - current : (to - current) * -1;

            DOTween
                .To(() => current, x => current = x, to, change * perUnitDelay)
                .SetEase(Ease.Linear)
                .OnUpdate(() => UpdateText(view, current))
                .OnComplete(() => onComplete?.Invoke());
        }


        private void UpdateText(Text view, int value) => 
            view.text = value.ToString();
    }
}
