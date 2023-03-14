using System.Collections.Generic;
using UnityEngine;

namespace Architecture.Services
{
    public class AssetProvider
    {
        private readonly Dictionary<string, GameObject> _cash = new Dictionary<string, GameObject>();
        
        public GameObject LoadObject(string path)
        {
            if (_cash.ContainsKey(path))
                return _cash[path];

            GameObject obj = Resources.Load(path) as GameObject;
            _cash.Add(path, obj);
            return obj;
        }
    }
}
