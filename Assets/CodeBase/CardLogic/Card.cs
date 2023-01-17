using System;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.CardLogic
{
    public class Card : MonoBehaviour
    {
        public event Action<Card> OnCardDestroyed;

        private int _currentHealth;
        private int _currentDamage;
        private int _currentMana;

        private ICardView _view;

        public void InitCard(int mana, int health, int damage, ICardView view)
        {
            _view = view;
            _currentMana = mana;
            _currentHealth = health;
            _currentDamage = damage;
        }
    
        public void ChangeDamageValue(int changeValue)
        {
            var from = _currentDamage;
            _currentDamage += changeValue;

            if (_currentDamage < 0)
                _currentDamage = 0;
            
            _view.ChangeDamageView(from, _currentDamage);
        }
    
        public void ChangeHealthValue(int healthValue)
        {
            var from = _currentHealth;
            _currentHealth += healthValue;
            
            if (_currentHealth > 0)
                _view.ChangeHealthView(from, _currentHealth);
            else
                _view.ChangeHealthView(from,_currentHealth, DestroyCard);
        }

        public void ChangeManaValue(int manaValue)
        {
            var from = _currentMana;
            _currentMana += manaValue;

            if (_currentMana < 0)
                _currentMana = 0;
            
            _view.ChangeManaView(from, _currentMana);
        }

        private void DestroyCard()
        {
            OnCardDestroyed?.Invoke(this);
            Destroy(gameObject);
        }
    }
}