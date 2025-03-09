using Dreamteck.Splines;
using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using static Dreamteck.Splines.LengthCalculator;

namespace RilixHalloweenChallenge
{
    public class RollerCoasterDashboard : MonoBehaviour
    {
        [SerializeField] SplineFollower _splineFollower;
        [SerializeField] LengthCalculator _lengthCalculator;
        [SerializeField] TMP_Text _splineLength;
        [SerializeField] TMP_Text _playerSpeed;

        bool _reachedEnd = false;

        void Start () 
        {
            SetSplineLengthText();
            SetPlayerSpeedText();

            _splineFollower.onMotionApplied += SetPlayerSpeedText;
        }

        private void OnDestroy()
        {
            _splineFollower.onMotionApplied -= SetPlayerSpeedText;
        }

        private void SetSplineLengthText() 
        {
            _splineLength.text = _lengthCalculator.length.ToString("0.0").Replace(',','.') + " m";
        }

        private void SetPlayerSpeedText() 
        {
            if (_reachedEnd) 
            {
                _playerSpeed.text =  "0.0 m/s";
                return;
            }
            _playerSpeed.text = _splineFollower.followSpeed.ToString("0.00") + " m/s";
        }

        public void OnReachedEnd() 
        {
            _reachedEnd = true;
        }

    }
}
