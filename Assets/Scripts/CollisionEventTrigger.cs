using UnityEngine;
using UnityEngine.Events;

namespace RilixHalloweenChallenge
{
    public class CollisionEventTrigger : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform _target;

        [Header("Events")]
        [SerializeField] UnityEvent _onTriggerEnter;

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform == _target)
            {
                _onTriggerEnter?.Invoke();
            }
        }
    }
}