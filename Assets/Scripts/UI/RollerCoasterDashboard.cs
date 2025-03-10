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
        [SerializeField] TMP_Text _currentPos;

        bool _reachedEnd = false;
        float _length = 0.0f;

        void Start () 
        {
            _length = _lengthCalculator.length;
            
            SetSplineLengthText();
            SetPlayerSpeedText();
            

            _splineFollower.onMotionApplied += SetPlayerSpeedText;
            _splineFollower.onMotionApplied += SetCurrentPosText;
        }

        private void OnDestroy()
        {
            _splineFollower.onMotionApplied -= SetPlayerSpeedText;
        }

        private void SetSplineLengthText() 
        {
            _splineLength.text = _length.ToString("0.0").Replace(',','.') + " m";
        }
        private void SetCurrentPosText()
        {
            _currentPos.text = (_splineFollower.GetPercent() * _length).ToString("0.0").Replace(',', '.') + " m";
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
