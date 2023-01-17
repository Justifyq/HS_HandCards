using System;
using System.Threading.Tasks;
using CodeBase.Data;

namespace CodeBase.Factory
{
    public interface ISpriteProvider : IDisposable
    { 
        Task<SpriteSeed> LoadSprite();
    }
}