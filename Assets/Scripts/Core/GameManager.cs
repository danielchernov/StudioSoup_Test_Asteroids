using UnityEngine;

namespace AsteroidsTest.Core
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("GameManager is Null");
                }

                return _instance;
            }
        }

        private void Awake()
        {
            _instance = this;
        }
    }
}
