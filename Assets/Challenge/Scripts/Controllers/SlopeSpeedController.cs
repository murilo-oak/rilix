using Dreamteck.Splines;
using UnityEngine;

namespace RilixHalloweenChallenge
{
    public class SlopeSpeedController : MonoBehaviour
    {
        [SerializeField] SplineFollower _splineFollower;
        [SerializeField] PlayerRideConfig _config;

        private void FixedUpdate()
        {
            if (_splineFollower == null || _config == null) return;

            ApplySpeedChanges(); 
        }

        void ApplySpeedChanges()
        {
            float newSpeed = _splineFollower.followSpeed + _config.gravityInfluence * GetInclinationFactor();
            newSpeed *= _config.speedDamping;

            _splineFollower.followSpeed = Mathf.Clamp(newSpeed, _config.minSpeed, _config.maxSpeed);
        }

        float GetInclinationFactor()
        {
            return Vector3.Dot(-Vector3.up, _splineFollower.transform.forward);
        }
    }
}