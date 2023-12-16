using UnityEngine;
using AsteroidsTest.Core;

namespace AsteroidsTest.Game
{
    public class CheckForBounds : MonoBehaviour
    {
        ScreenBounds _screenBounds;

        float _teleportTimer = 0;

        void Start()
        {
            if (_screenBounds == null)
            {
                _screenBounds = GameObject
                    .FindGameObjectWithTag("ScreenBounds")
                    .GetComponent<ScreenBounds>();
            }
        }

        void Update()
        {
            _teleportTimer -= Time.deltaTime;

            // Teleport if Out Of Bounds
            if (_teleportTimer <= 0 && _screenBounds.isOutOfBounds(transform.position))
            {
                Vector2 newPosition = _screenBounds.CalculateWrappedPosition(transform.position);
                transform.position = newPosition;

                _teleportTimer = 1;
            }
        }
    }
}
