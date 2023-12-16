using UnityEngine;

namespace AsteroidsTest.Core
{
    public class UIManager : MonoBehaviour
    {
        private static UIManager _instance;
        public static UIManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("UIManager is Null");
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
