using UnityEngine;

namespace CodeBase.Data
{
    public class CardInfo
    {
        public readonly Sprite CardSprite;
        public readonly string Description;
        public readonly int ManaCost;
        public readonly int DamageValue;
        public readonly int HealthValue;
        public readonly string CardName;
    
        public CardInfo(Sprite cardSprite, string cardName, string description, int manaCost, int damageValue, int healthValue)
        {
            CardSprite = cardSprite;
            CardName = cardName;
            Description = description;
            ManaCost = manaCost;
            DamageValue = damageValue;
            HealthValue = healthValue;
        }
    }
}