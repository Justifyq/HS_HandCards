using UnityEngine;

namespace CodeBase.Data
{
    public struct SpriteSeed
    {
        public readonly string Seed;
        public readonly Sprite Sprite;
        
        public SpriteSeed(Sprite sprite, string seed)
        {
            Seed = seed;
            Sprite = sprite;
        }
    }
}