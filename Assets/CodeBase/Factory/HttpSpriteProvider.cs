using System;
using System.Threading;
using System.Threading.Tasks;
using CodeBase.Data;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Random = UnityEngine.Random;

namespace CodeBase.Factory
{
    public class HttpSpriteProvider : ISpriteProvider
    {
        private const int MIN_RANDOM = 3;
        private const int MAX_RANDOM = 20;
        
        private const string SYMBOLS = "abcdefghijklmnopqrstuvwxyz0123456789";
        private const string URL_FORMAT = "https://picsum.photos/seed/{0}/200/300";

        private readonly CancellationTokenSource _tokenSource;
        private readonly Vector2 _pivot = new Vector2(.5f, .5f);
        
        private bool _isDisposed;
        private string _lastSeed;
        private UnityWebRequest _request;
        
        public HttpSpriteProvider() => 
            _tokenSource = new CancellationTokenSource();

        public async Task<SpriteSeed> LoadSprite()
        {
            _request?.Dispose();
            _request = UnityWebRequestTexture.GetTexture(GetUrl());
            await _request.SendWebRequest();
            return new SpriteSeed(GetTextureFromRequest(), _lastSeed);
        }
        
        public void Dispose()
        {
            if (_isDisposed)
                return;
        
            _isDisposed = true;
        
            _request.Dispose();
            _tokenSource.Dispose();

            GC.SuppressFinalize(this);
        }
        
        private Sprite GetTextureFromRequest()
        {
            var texture =  DownloadHandlerTexture.GetContent(_request);
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), _pivot);
        }

        private string GetUrl() => 
            string.Format(URL_FORMAT, GetRandomSeed());

        private string GetRandomSeed()
        {
            var charAmount = Random.Range(MIN_RANDOM, MAX_RANDOM);
            var seed = string.Empty;
        
            for (var i = 0; i < charAmount; i++)
                seed += SYMBOLS[Random.Range(0, SYMBOLS.Length)];

            _lastSeed = seed;
            return seed;
        }

        ~HttpSpriteProvider() => Dispose();
    }
}