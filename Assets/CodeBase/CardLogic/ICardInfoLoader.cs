using System.Threading.Tasks;
using CodeBase.Data;

namespace CodeBase.CardLogic
{
    public interface ICardInfoLoader
    {
        Task<CardInfo[]> LoadCardInfo();
    }
}