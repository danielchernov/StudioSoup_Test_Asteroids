using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsTest.Game
{
    public class ObjectPooler : MonoBehaviour
    {
        [System.Serializable]
        public class Pool
        {
            public string PoolTag;
            public GameObject ObjectPrefab;
            public int PoolSize;
        }

        private static ObjectPooler _instance;
        public static ObjectPooler Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("ObjectPooler is Null");
                }

                return _instance;
            }
        }

        [SerializeField]
        List<Pool> _pools;

        [SerializeField]
        Dictionary<string, Queue<GameObject>> _poolDictionary;

        private void Awake()
        {
            _instance = this;
        }

        private void Start()
        {
            // Create Pools
            _poolDictionary = new Dictionary<string, Queue<GameObject>>();

            foreach (Pool pool in _pools)
            {
                Queue<GameObject> objectPool = new Queue<GameObject>();

                for (int i = 0; i < pool.PoolSize; i++)
                {
                    GameObject obj = Instantiate(pool.ObjectPrefab);
                    obj.SetActive(false);

                    objectPool.Enqueue(obj);

                    if (pool.PoolTag == "Bullets")
                        obj.transform.SetParent(transform.GetChild(0));
                    else if (pool.PoolTag == "AsteroidSmall")
                        obj.transform.SetParent(transform.GetChild(1));
                    else if (pool.PoolTag == "AsteroidMedium")
                        obj.transform.SetParent(transform.GetChild(2));
                    else if (pool.PoolTag == "AsteroidLarge")
                        obj.transform.SetParent(transform.GetChild(3));
                }

                _poolDictionary.Add(pool.PoolTag, objectPool);
            }
        }

        // Spawn Pool Object Function
        public GameObject SpawnFromPool(
            string poolTag,
            Vector3 spawnPosition,
            Quaternion spawnRotation
        )
        {
            if (!_poolDictionary.ContainsKey(poolTag))
            {
                Debug.LogError("Pool Tag doesn't exist in the Dictionary");
                return null;
            }

            GameObject objectToSpawn = _poolDictionary[poolTag].Dequeue();

            objectToSpawn.transform.position = spawnPosition;
            objectToSpawn.transform.rotation = spawnRotation;
            objectToSpawn.SetActive(true);

            _poolDictionary[poolTag].Enqueue(objectToSpawn);

            return objectToSpawn;
        }
    }
}
