using System;
using CodeBase.CardLogic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.UI
{
    public class CardRandomValueChanger : MonoBehaviour
    {
        private const int MIN_CHANGE_INCLUSIVE = -2;
        private const int MAX_CHANGE_EXCLUSIVE = 9;
        
        public event Action OnInitialized;
        public event Action OnValueChanged;
        
        private readonly int _maxEnumRandom = Enum.GetNames(typeof(CharacteristicType)).Length;
        
        private Card _card;
        private bool _isInit;
        
        public void ChangeCardValue() => 
            ChangeRandomValue(_card);

        public void UpdateCard(Card card)
        {
            TryInit();
            _card = card;
        }

        private void TryInit()
        {
            if (_isInit)
                return;

            _isInit = true;
            OnInitialized?.Invoke();
        }

        private void ChangeRandomValue(Card card)
        {
            if (card == null)
                return;
            
            switch (GetRandomCharacteristicType())
            {
                case CharacteristicType.Mana :
                    card.ChangeManaValue(GetRandomValue());
                    break;
                case CharacteristicType.Health :
                        card.ChangeHealthValue(GetRandomValue());
                        break;
                case CharacteristicType.Damage :
                        card.ChangeDamageValue(GetRandomValue());
                        break;
            }
            
            OnValueChanged?.Invoke();
        }
        
        private int GetRandomValue() => 
                Random.Range(MIN_CHANGE_INCLUSIVE,  MAX_CHANGE_EXCLUSIVE);

        private CharacteristicType GetRandomCharacteristicType() => 
            (CharacteristicType)Random.Range(0, _maxEnumRandom);

        private enum CharacteristicType
        {
            Mana = 0,
            Health = 1,
            Damage = 2,
        }
    }
}