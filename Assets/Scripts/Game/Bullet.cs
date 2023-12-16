using System.Collections;
using UnityEngine;
using AsteroidsTest.Core;

namespace AsteroidsTest.Game
{
    public class Bullet : MonoBehaviour, IPerishable
    {
        [SerializeField]
        float _bulletForce = 10;

        [SerializeField]
        Rigidbody2D _bulletRb;

        private void OnEnable()
        {
            // Add Forward Force
            _bulletRb.velocity = Vector2.zero;
            Vector2 bulletDirection = transform.up;

            _bulletRb.AddForce(bulletDirection * _bulletForce, ForceMode2D.Impulse);

            // Set Deactivate After Time
            StartCoroutine(DeactivateAfterTime(2));
        }

        public IEnumerator DeactivateAfterTime(float timeToDeactivate)
        {
            yield return new WaitForSeconds(timeToDeactivate);
            gameObject.SetActive(false);
        }
    }
}
