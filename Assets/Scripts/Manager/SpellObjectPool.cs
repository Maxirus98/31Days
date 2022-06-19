using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Manager {
    /// <summary>  
    /// Object Pool for SpawnSpells
    /// </summary>
    public class SpellObjectPool : MonoBehaviour
    {
        public static SpellObjectPool Instance;
        private Dictionary<string, GameObject> _pooledObjects;

        /// <summary>
        /// List of objects to pool and how many of this object should be pooled;
        /// </summary>
        public List<GameObject> objectsToPool;
        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _pooledObjects = new Dictionary<string, GameObject>();
            foreach (var (go, index) in objectsToPool.WithIndex())
            {
                var tmp = Instantiate(go, transform);
                tmp.SetActive(false);
                AddObjectToPool(go, go.name + index);
            }
        }

        /// <summary>
        /// Retrieve the first gameObject by it's name and if it's inactive
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public GameObject GetPooledObjectByKey(string key)
        {
            return _pooledObjects.FirstOrDefault(po => po.Key.Contains(key) && !po.Value.activeInHierarchy).Value;
        }

        /// <summary>
        /// Add an object to the pool of gameObjects. This should only be called in an Awake method
        /// </summary>
        /// <param name="go">The GameObject to instantiate on Start</param>
        /// <param name="key">The Unique Key accessor for the Dictionary</param>
        public void AddObjectToPool(GameObject go, string key = "")
        {
            if (key == string.Empty) key = go.name;
            _pooledObjects.Add(key, go);
        }
    }
}