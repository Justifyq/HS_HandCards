using CodeBase.CardLogic;
using UnityEngine;

namespace CodeBase.Factory
{
    public interface ICardFactory
    {
        Card[] CreateCards(Transform parent = null);
    }
}