using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RilixHalloweenChallenge
{
    public class InclinationSpeedController : MonoBehaviour
    {

        [SerializeField] SplineFollower _splineFollower;

        [Range(0, 1)]
        [SerializeField] float _gravityInfluence;

        [SerializeField] float _minSpeed;
        [SerializeField] float _maxSpeed;
        
        [Range(0, 1)]
        [SerializeField] float _speedDamping;


        private void FixedUpdate()
        {
            ApplySpeedChanges();
        }

        void ApplySpeedChanges() 
        {
            float newSpeed = _splineFollower.followSpeed + _gravityInfluence * GetInclinationFactor();
            newSpeed *= _speedDamping;

            _splineFollower.followSpeed = Mathf.Clamp(newSpeed, _minSpeed, _maxSpeed);
        }

        float GetInclinationFactor()
        {
            return Vector3.Dot(-Vector3.up, _splineFollower.transform.forward);
        }
    }
}
