using System.Collections;
using UnityEngine;

namespace AsteroidsTest.Game
{
    public class Bullet : MonoBehaviour, IPerishable
    {
        [SerializeField]
        float _bulletForce = 10;

        [SerializeField]
        float _timeToDeactivate = 2;

        [SerializeField]
        Rigidbody2D _bulletRb;

        private void OnEnable()
        {
            // Add Forward Force
            if (_bulletRb == null)
                return;

            _bulletRb.velocity = Vector2.zero;
            Vector2 bulletDirection = transform.up;

            _bulletRb.AddForce(bulletDirection * _bulletForce, ForceMode2D.Impulse);

            // Set Deactivate After Time
            StartCoroutine(DeactivateAfterTime(_timeToDeactivate));
        }

        void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.tag == "Asteroid")
            {
                collider.GetComponent<AsteroidController>().TakeDamage(1);
                gameObject.SetActive(false);
            }
        }

        public IEnumerator DeactivateAfterTime(float timeToDeactivate)
        {
            yield return new WaitForSeconds(timeToDeactivate);
            gameObject.SetActive(false);
        }
    }
}
